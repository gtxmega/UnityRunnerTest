using GameTypes;
using Services.Events;
using Services.Factory;
using Services.Locator;
using UnityEngine;

namespace Levels.StateMachine.States
{
    public class LoadLevelState : ILevelState
    {
        private readonly LevelStateMachine _stateMachine;
        private readonly IGameEventsExec _gameEventsExec;
        private readonly LevelConstants _levelConstants;
        private readonly IGameFactory _gameFactory;
        private readonly LevelStaticData _levelStaticData;

        public LoadLevelState(LevelStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;

            _gameEventsExec = serviceLocator.GetService<IGameEventsExec>();
            _levelConstants = serviceLocator.GetService<LevelConstants>();
            _gameFactory = serviceLocator.GetService<IGameFactory>();
            _levelStaticData = serviceLocator.GetService<LevelStaticData>();
        }

        public void Enter()
        {
            OnSpawnMainActor();

            _gameEventsExec.OnLevelLoaded();

            _stateMachine.EnterIn<InitializeLevelState>();
        }

        private void OnSpawnMainActor()
        {
            GameObject spawnPoint = GameObject.FindGameObjectWithTag(_levelConstants.SpawnPointTag);

            Actor playerActor = _gameFactory.CreateActor(_levelConstants.PlayerActorPrefab, 
                                                                spawnPoint.transform.position);

            playerActor.SwitchTeam(_levelConstants.PlayerTeam);

            _levelStaticData.SetPlayerMainActor(playerActor);
        }

        public void Exit()
        {
            
        }
    }
}