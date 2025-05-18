using AISensors;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace AIStateMachine.AIStates
{
    public class AiStateWatch : MonoBehaviour, IState
    {
        private NavMeshAgent _navMeshAgent;
        private PlayerProximityDetectorSensor _playerProximityDetectorSensor;
        [SerializeField] private float turnSpeed = 3.0f;
        private GameObject _parent;
        private void Start()
        {
            _parent = transform.parent.gameObject;
            _navMeshAgent = _parent.GetComponent<NavMeshAgent>();
            _playerProximityDetectorSensor = GetComponent<PlayerProximityDetectorSensor>();
        }

        public StateId GetId()
        {
            return StateId.Watch;
        }

        public void StateEnter(AiAgent agent)
        {
            _navMeshAgent.ResetPath();
        }

        public void StateUpdate(AiAgent agent)
        {
            // if no detected player then something is clearly very wrong
            if (!_playerProximityDetectorSensor?.DetectedPlayer) return;
            
            // determine vector to the player
            var vectorToPlayer = _playerProximityDetectorSensor.DetectedPlayer.transform.position - _parent.transform.position;
            // rotate towards
            Vector3 newDirection = Vector3.RotateTowards(_parent.transform.forward, vectorToPlayer, Time.fixedDeltaTime/turnSpeed, 0.0f);
            _parent.transform.rotation = Quaternion.LookRotation(newDirection);
        }

        public void StateExit(AiAgent agent)
        {
            
        }
    }
}