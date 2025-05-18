using System;
using AIStateMachine.AIStates;
using AIStateMachine.Predicates;
using UnityEngine;

namespace AIStateMachine
{
    [Serializable]
    public class StateTransition: MonoBehaviour
    {
        public StateId transitionFrom;
        public StateId transitionTo;
        public BasePredicate predicate;
        [SerializeField] private bool not;
        public bool Evaluate()
        {
            bool result = predicate.Evaluate();
            
            return not ? !result : result;
        }
        
        
    }
}