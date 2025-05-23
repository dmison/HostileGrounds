using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace AIStateMachine.AIStates
{
    public class AiStateWander : MonoBehaviour, IState
    {
        public float range = 100.0f; //radius of sphere
        [SerializeField] private NavMeshAgent navMeshAgent;

        public StateId GetId()
        {
            return StateId.Wander;
        }

        public void StateEnter(AiAgent agent)
        {
            navMeshAgent.ResetPath();
        }

        public void StateUpdate(AiAgent agent)
        {
            if (!(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)) return; //done with path
            
            Vector3 point;
            if (RandomPoint(transform.position, range, out point)) //pass in our centre point and radius of area
            {
                navMeshAgent.SetDestination(point);
            }
        }

        public void StateExit(AiAgent agent)
        {
            
            navMeshAgent.ResetPath();
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
