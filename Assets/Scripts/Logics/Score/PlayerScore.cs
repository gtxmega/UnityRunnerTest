using Services.Events;
using Services.Locator;
using UnityEngine;

namespace Logics.Score
{
    public class PlayerScore : MonoBehaviour, IInjectServices
    {
        public int Score => _current;

        private int _current;

        private IGameEventsExec _gameEventsExec;

        public void InjectServices(IServiceLocator serviceLocator)
        {
            _gameEventsExec = serviceLocator.GetService<IGameEventsExec>();
        }

        public void AddPoints(int amount)
        {
            _current += amount;

            _gameEventsExec.OnPlayerReceivedPoints(_current, amount);
        }
    }
}