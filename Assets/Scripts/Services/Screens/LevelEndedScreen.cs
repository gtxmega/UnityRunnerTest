using Logics.Score;
using Services.Events;
using Services.Locator;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Services.Screens
{
    public class LevelEndedScreen : MonoBehaviour, IInjectServices
    {
        [SerializeField] private Canvas _endedScreenCanvas;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private float _speedText;

        private IGameEvents _gameEvents;
        private PlayerScore _playerScore;

        public void InjectServices(IServiceLocator serviceLocator)
        {
            _gameEvents = serviceLocator.GetService<IGameEvents>();
            _playerScore = serviceLocator.GetService<PlayerScore>();

            _gameEvents.LevelEnded += OnLevelEnded;
        }

        private void OnLevelEnded()
        {
            StartCoroutine(ScoreTextAnim());

            _endedScreenCanvas.enabled = true;
        }

        private IEnumerator ScoreTextAnim()
        {
            float count = 0.0f;

            while (true) 
            {
                count += _speedText * Time.deltaTime;

                count = Mathf.Clamp(count, 0.0f, _playerScore.Score);

                _scoreText.text = "Score: " + Mathf.Round(count).ToString();

                yield return null;
            }
        }


        private void OnDestroy()
        {
            _gameEvents.LevelEnded -= OnLevelEnded;
        }
    }
}