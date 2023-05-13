using GameTypes;
using Logics.Score;
using Services.Locator;
using TMPro;
using UnityEngine;

namespace Logics.Interactables.Variants
{
    public class AddScoreTrigger : MonoBehaviour, IInteractive, IInjectServices
    {
        [SerializeField] private int _addCount;
        [SerializeField] private TMP_Text _displayText;

        private PlayerScore _playerScore;

        public void InjectServices(IServiceLocator serviceLocator)
        {
            _playerScore = serviceLocator.GetService<PlayerScore>();
        }

        private void Start()
        {
            _displayText.text = _addCount.ToString();
        }

        public void OnCollideAt(Actor actor)
        {
            _playerScore.AddPoints(_addCount);
            Destroy(gameObject);
        }

        public void OnOverlapExit(Actor actor)
        {
            
        }
    }
}