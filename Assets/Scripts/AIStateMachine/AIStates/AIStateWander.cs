using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AIStateMachine.AIStates
{
    public class AiStateWander : MonoBehaviour, IState
    {
        public float range = 100.0f; //radius of sphere
        private NavMeshAgent _navMeshAgent;

        private void Start()
        {
            _navMeshAgent = GetComponentInParent<NavMeshAgent>();
        }

        public StateId GetId()
        {
            return StateId.Wander;
        }

        public void StateEnter(AiAgent agent)
        {
            _navMeshAgent.ResetPath();
        }

        public void StateUpdate(AiAgent agent)
        {
            if (!(_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)) return; //done with path
            
            Vector3 point;
            if (RandomPoint(transform.position, range, out point)) //pass in our centre point and radius of area
            {
                _navMeshAgent.SetDestination(point);
            }
        }

        public void StateExit(AiAgent agent)
        {
            
            _navMeshAgent.ResetPath();
        }
        
        private bool RandomPoint(Vector3 center, float distance, out Vector3 result)
        {

            Vector3 randomPoint = center + Random.insideUnitSphere * distance; //random point in a sphere 
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
            { 
                result = hit.position;
                return true;
            }
            result = Vector3.zero;
            return false;
        }
    }
}
