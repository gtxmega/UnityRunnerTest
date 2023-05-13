using GameTypes;
using Levels;
using Services.Events;
using Services.Factory;
using Services.Locator;
using System.Collections;
using UnityEngine;

namespace Logics.Platforms
{
    public class PlatformSpawner : MonoBehaviour, IInjectServices
    {
        [Header("References")]
        [SerializeField] private Platform _currentPlatform;
        [SerializeField] private Platform[] _platformList;

        [Header("Settings")]
        [SerializeField] private int _countSpawnPlatform;
        [SerializeField] private float _neededDistanceToSpawn;

        private IGameEvents _gameEvents;
        private IGameEventsExec _gameEventsExec;
        private IGameFactory _gameFactory;
        private LevelConstants _levelConstants;

        private Actor _playerActor;
        private int _currentPlatformIndex;

        private int _countSpawnedPlaftorms;

        public void InjectServices(IServiceLocator serviceLocator)
        {
            _gameEvents = serviceLocator.GetService<IGameEvents>();
            _gameEventsExec = serviceLocator.GetService<IGameEventsExec>();
            _gameFactory = serviceLocator.GetService<IGameFactory>();
            _levelConstants = serviceLocator.GetService<LevelConstants>();

            _gameEvents.LevelStart += OnLevelStart;
            _gameEvents.ActorSpawned += OnActorSpawned;
        }

        private void OnActorSpawned(Actor actor)
        {
            if(actor.IsMainActor &&  actor.Team == _levelConstants.PlayerTeam)
            {
                _playerActor = actor;
                _gameEvents.ActorSpawned -= OnActorSpawned;
            }
        }

        private void OnLevelStart()
        {
            StartCoroutine(SpawnPlatforms());
        }

        private IEnumerator SpawnPlatforms()
        {
            while (true)
            {
                FixPlatformRangeIndex();

                Vector3 currentPlatoformPosition = _currentPlatform.GetPosition();

                float sqrDistance = (_playerActor.GetPosition() - currentPlatoformPosition).sqrMagnitude;

                if(_neededDistanceToSpawn >= sqrDistance)
                {
                    for (int i = 0; i < _countSpawnPlatform; ++i)
                    {
                        FixPlatformRangeIndex();

                        currentPlatoformPosition.z += _currentPlatform.GetSizeZ();

                        Platform plaftorm = _gameFactory.CreatePlatform(_platformList[_currentPlatformIndex], 
                                                                            currentPlatoformPosition);

                        _currentPlatform = plaftorm;

                        _currentPlatformIndex = Random.Range(0, _platformList.Length);

                        ++_countSpawnedPlaftorms;
                    }
                }

                yield return null;

                if(_countSpawnedPlaftorms >= _levelConstants.WinPlatformsCount)
                {
                    _gameEventsExec.OnLevelEnded();
                    yield break;
                }
            }
        }

        private void FixPlatformRangeIndex()
        {
            if (_currentPlatformIndex >= _platformList.Length)
                _currentPlatformIndex = 0;
        }

        private void OnDestroy()
        {
            _gameEvents.LevelStart -= OnLevelStart;
        }
    }
}