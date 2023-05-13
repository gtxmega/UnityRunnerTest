using GameTypes;
using Services.Events;
using Services.Locator;
using UnityEngine;

namespace Logics.CameraBehavior
{
    public class PlayerFollowCamera : MonoBehaviour, IInjectServices
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Vector3 _cameraOffset;
        [SerializeField] private float _smoothSpeed;
        [SerializeField] private ETeam _playerTeam;

        private Transform _playerTransform;

        private IGameEvents _gameEvents;

        public void InjectServices(IServiceLocator serviceLocator)
        {
            _gameEvents = serviceLocator.GetService<IGameEvents>();

            _gameEvents.ActorSpawned += OnActorSpawned;
        }

        private void OnActorSpawned(Actor actor)
        {
            if(actor.Team == _playerTeam && actor.IsMainActor)
            {
                _playerTransform = actor.SelftTransform;
                _gameEvents.ActorSpawned -= OnActorSpawned;
            }
        }

        private void LateUpdate()
        {
            if(_playerTransform != null)
            {
                Vector3 deltaPosition = _playerTransform.position;
                deltaPosition += _cameraOffset;

                _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, deltaPosition, _smoothSpeed);
            }
        }


        private void OnDestroy()
        {
            _gameEvents.ActorSpawned -= OnActorSpawned;
        }
    }
}