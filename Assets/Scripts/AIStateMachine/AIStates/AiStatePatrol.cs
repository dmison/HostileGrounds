using UnityEngine;
using UnityEngine.AI;
using WayPoints;


namespace AIStateMachine.AIStates
{
    public class AiStatePatrol : MonoBehaviour, IState
    {
        [SerializeField] private GameObject body;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private PatrolPath patrolPath;
        private Vector3 _currentWayPoint = Vector3.negativeInfinity;
        private int _currentWayPointIndex;
        public StateId GetId()
        {
            return StateId.Patrol;
        }

        public void StateEnter(AiAgent agent)
        {
            if (_currentWayPoint.Equals(Vector3.negativeInfinity))
            {
                (_currentWayPoint, _currentWayPointIndex) = patrolPath.GetClosestWayPoint(transform.position);
                navMeshAgent.ResetPath();    
            }
            navMeshAgent.SetDestination(_currentWayPoint);
            navMeshAgent.isStopped = false;
        }

        public void StateUpdate(AiAgent agent)
        {
            Debug.DrawLine(body.transform.position, _currentWayPoint, Color.green);
            if (!(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)) return; //done with path
            (_currentWayPoint, _currentWayPointIndex) = patrolPath.GetNextWayPoint(_currentWayPointIndex);
               
            navMeshAgent.SetDestination(_currentWayPoint);
        }

        public void StateExit(AiAgent agent)
        {
            navMeshAgent.isStopped = true;
        }
        
    }
}
