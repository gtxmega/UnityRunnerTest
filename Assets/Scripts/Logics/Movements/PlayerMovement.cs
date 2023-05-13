using Services.Events;
using Services.GameInput;
using Services.Locator;
using System.Collections;
using UnityEngine;

namespace Logics.Movements
{
    public class PlayerMovement : MonoBehaviour, IInjectServices
    {
        [SerializeField] private Transform _selfTransform;
        [SerializeField] private float _speed;
        [SerializeField] private float _restoreSpeed;

        [Header("Push back")]
        [SerializeField] private float _pushBackPower;
        [SerializeField] private float _pushBackDuration;
        [SerializeField] private float _pushBackDecreaseSpeed;

        [Header("Lines")]
        [SerializeField] private float[] _linesPositionX;
        [SerializeField] private float _switchSmooth;
        [SerializeField] private float _switchDelay;
        
        private bool _isReadySwitch = true;
        private int _currentLineID = 1;

        private Vector3 _velocity;
        private Coroutine _pushBackCorountine;

        private IInputHandler _inputHandler;
        private IGameEvents _gameEvents;

        private bool _isStopped;

        public void InjectServices(IServiceLocator serviceLocator)
        {
            _inputHandler = serviceLocator.GetService<IInputHandler>();
            _gameEvents = serviceLocator.GetService<IGameEvents>();

            _gameEvents.LevelEnded += OnLevelEnded;
        }

        private void OnLevelEnded()
        {
            _isStopped = true;

            StopAllCoroutines();
        }

        private void Start()
        {
            _velocity = new Vector3(0.0f, 0.0f, _speed);
        }

        private void Update()
        {
            if (_isStopped) return;

            if (_isReadySwitch)
            {
                if (_inputHandler.SwipeLeft())
                {
                    SwipeLeft();
                }

                if (_inputHandler.SwipeRight())
                {
                    SwipeRight();
                }
            }

            _selfTransform.position += _velocity * Time.deltaTime;

            Vector3 originPosition = _selfTransform.position;

            originPosition.x = _linesPositionX[_currentLineID];
            _selfTransform.position = Vector3.Lerp(_selfTransform.position, originPosition, _switchSmooth);
        }

        private void SwipeRight()
        {
            if (_currentLineID == 0)
            {
                ++_currentLineID;
            }
            else if (_currentLineID == 1)
            {
                ++_currentLineID;
            }

            StartDelayTimer();
        }

        private void SwipeLeft()
        {
            if (_currentLineID == 2)
            {
                --_currentLineID;
            }
            else if (_currentLineID == 1)
            {
                --_currentLineID;
            }

            StartDelayTimer();
        }

        public void OnPushBack()
        {
            if(_pushBackCorountine != null)
            {
                StopCoroutine(_pushBackCorountine);
                _pushBackCorountine = null;
            }

            _pushBackCorountine = StartCoroutine(PushBack(_pushBackPower));
        }

        private IEnumerator PushBack(float power)
        {
            float durationTimer = _pushBackDuration;

            power *= -1.0f;

            _velocity.z = 0.0f;
            _velocity.z = power;

            while (durationTimer > 0.0f)
            {
                _velocity.z += _pushBackDecreaseSpeed * Time.deltaTime;

                _velocity.z = Mathf.Clamp(_velocity.z, power, 0.0f);

                durationTimer -= Time.deltaTime;
                yield return null;
            }

            while (_velocity.z < _speed)
            {
                _velocity.z += _restoreSpeed * Time.deltaTime;

                _velocity.z = Mathf.Clamp(_velocity.z, power, _speed);

                yield return null;
            }

            _pushBackCorountine = null;
        }

        private void StartDelayTimer()
        {
            _isReadySwitch = false;
            StartCoroutine(SwitchDelay());
        }

        private IEnumerator SwitchDelay()
        {
            yield return new WaitForSeconds(_switchDelay);
            _isReadySwitch = true;
        }

        private void OnDestroy()
        {
            _gameEvents.LevelEnded -= OnLevelEnded;
        }
    }
}