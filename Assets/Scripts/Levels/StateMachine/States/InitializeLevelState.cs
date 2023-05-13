﻿using Services.Events;
using Services.Locator;

namespace Levels.StateMachine.States
{
    public class InitializeLevelState : ILevelState
    {
        private readonly LevelStateMachine _stateMachine;
        private readonly IGameEventsExec _gameEventsExec;

        public InitializeLevelState(LevelStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;

            _gameEventsExec = serviceLocator.GetService<IGameEventsExec>();

        }

        public void Enter()
        {
            _gameEventsExec.OnLevelInitialized();

            _stateMachine.EnterIn<LoopLevelState>();
        }

        public void Exit()
        {
            
        }
    }
}