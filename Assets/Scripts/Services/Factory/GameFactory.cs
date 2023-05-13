using GameTypes;
using Services.Events;
using Services.Locator;
using UnityEngine;

namespace Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly IGameEventsExec _gameEventsExec;

        public GameFactory(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
            _gameEventsExec = serviceLocator.GetService<IGameEventsExec>();
        }

        public Actor CreateActor(Actor prefab, Vector3 position)
        {
            Actor actorInstance = Object.Instantiate(prefab, position, Quaternion.identity);

            TryInjectServices(actorInstance.gameObject);

            _gameEventsExec.OnActorSpawned(actorInstance);

            return actorInstance;
        }

        public Platform CreatePlatform(Platform prefab, Vector3 position) 
        {
            Platform platformInstance = Object.Instantiate(prefab, position, Quaternion.identity);

            TryInjectServices(platformInstance.gameObject);

            return platformInstance;
        }

        private void TryInjectServices(GameObject root)
        {
            foreach (var component in root.GetComponentsInChildren<IInjectServices>())
            {
                component.InjectServices(_serviceLocator);
            }
        }
    }
}