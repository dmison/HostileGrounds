using System;
using UnityEngine;

namespace AIStateMachine.Predicates
{
    [Serializable]
    public abstract class BasePredicate: MonoBehaviour
    {
        public abstract bool Evaluate();
    }
}