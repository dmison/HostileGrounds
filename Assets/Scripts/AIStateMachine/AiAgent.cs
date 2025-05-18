using System.Collections.Generic;
using AIStateMachine.AIStates;
using UnityEngine;

namespace AIStateMachine
{
    public class AiAgent : MonoBehaviour
    {
        private StateMachine _stateMachine;
        [SerializeField] private StateId initialState;
        private List<IState> _states = new List<IState>();
        // TODO: should be in dictionary keyed on transitionFrom Id
        private List<StateTransition> _transitions = new List<StateTransition>();
        
        private void Start()
        {
            _stateMachine = new StateMachine(this);
            // collect all the states & transitions
            _states = new List<IState>(GetComponents<IState>());
            _transitions = new List<StateTransition>(GetComponents<StateTransition>());
            
            // add them to the state machine
            foreach (var state in _states)
            {
                _stateMachine.RegisterState(state);
            }

            foreach (var stateTransition in _transitions)
            {
                _stateMachine.RegisterTransition(stateTransition);
            }
            
            _stateMachine.ChangeState(initialState);
        }
        
        private void Update()
        {
            _stateMachine.Update();
        }


    }
}
