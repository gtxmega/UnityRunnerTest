using Services.Events;
using Services.Locator;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Logics.Score
{
    public class PlayerDisplayScore : MonoBehaviour, IInjectServices
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _addedText;
        [SerializeField] private float _addedTextTimer;

        private IGameEvents _gameEvents;
        private Coroutine _showAddedTextCoroutine;

        public void InjectServices(IServiceLocator serviceLocator)
        {
            _gameEvents = serviceLocator.GetService<IGameEvents>();

            _gameEvents.PlayerReceivedPoints += OnPlayerReceivedPoints;
        }

        private void OnPlayerReceivedPoints(int current, int amount)
        {
            _scoreText.text = current.ToString();

            if (_showAddedTextCoroutine != null) 
            {
                StopCoroutine(_showAddedTextCoroutine);
                _showAddedTextCoroutine = null;
            }

            _showAddedTextCoroutine = StartCoroutine(ShowAddedText(amount));
        }

        private IEnumerator ShowAddedText(int amount)
        {
            _addedText.text = "+" + amount.ToString();

            yield return new WaitForSeconds(_addedTextTimer);

            _addedText.text = string.Empty;

            _showAddedTextCoroutine = null;
        }

        private void OnDestroy()
        {
            _gameEvents.PlayerReceivedPoints -= OnPlayerReceivedPoints;
        }
    }
}