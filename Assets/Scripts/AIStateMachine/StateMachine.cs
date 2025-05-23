using System.Collections.Generic;
using AIStateMachine.AIStates;
using UnityEngine;

namespace AIStateMachine
{
    public class StateMachine
    {
        private readonly IState[] _states;
        private readonly AiAgent _agent;
        private StateId _currentState;
        private List<StateTransition> _transitions = new List<StateTransition>();

        private void CheckTransitions()
        {
            foreach (var stateTransition in _transitions)
            {
                if (stateTransition.transitionFrom == _currentState && stateTransition.Evaluate())
                {
                    ChangeState(stateTransition.transitionTo);
                    return;
                }
            }
            
        }
        //constructor
        public StateMachine(AiAgent agent)
        {
            _agent = agent;
            int numStates = System.Enum.GetNames(typeof(StateId)).Length;
            _states = new IState[numStates];
        }

        public void RegisterState(IState state)
        {
            int index = (int)state.GetId();
            _states[index] = state;
        }

        public void RegisterTransition(StateTransition transition)
        {
            _transitions.Add(transition);   
        }

        private IState GetState(StateId stateId)
        {
            int index = (int)stateId;
            return _states[index];
        }

        public void Update()
        {
            CheckTransitions();
            GetState(_currentState)?.StateUpdate(_agent);
        }

        public void ChangeState(StateId newState)
        {
            GetState(_currentState)?.StateExit(_agent);
            _currentState = newState;
            GetState(_currentState)?.StateEnter(_agent);
        }
    }
}
