using System;
using System.Collections.Generic;

namespace Levels.StateMachine
{
    public class LevelStateMachine
    {
        private readonly Dictionary<Type, ILevelState> _states;

        private ILevelState _currentState;

        public LevelStateMachine()
        {
            _states = new();
        }

        public LevelStateMachine AddLevelState(ILevelState state)
        {
            Type stateType = state.GetType();

            if(_states.ContainsKey(stateType) == false)
            {
                _states.Add(stateType, state);
            }

            return this;
        }

        public void EnterIn<T>() where T : ILevelState
        {
            if(_states.TryGetValue(typeof(T), out ILevelState state))
            {
                _currentState?.Exit();
                _currentState = state;
                _currentState.Enter();
            }
        }
    }
}