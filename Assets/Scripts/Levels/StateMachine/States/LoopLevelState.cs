using Services.Events;
using Services.Locator;

namespace Levels.StateMachine.States
{
    public class LoopLevelState : ILevelState
    {
        private readonly LevelStateMachine _stateMachine;
        private readonly IGameEventsExec _gameEventsExec;

        public LoopLevelState(LevelStateMachine stateMachine, IServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;

            _gameEventsExec = serviceLocator.GetService<IGameEventsExec>();
        }

        public void Enter()
        {
            _gameEventsExec.OnLevelStart();
        }

        public void Exit()
        {
            
        }
    }
}