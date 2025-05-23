using AISensors;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace AIStateMachine.AIStates
{
    public class AiStateWatch : MonoBehaviour, IState
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private GameObject body;
        private VisionSensor _visionSensor;
        [SerializeField] private float turnSpeed = 3.0f;
        
        private void Start()
        {
            _visionSensor = GetComponent<VisionSensor>();
        }

        public StateId GetId()
        {
            return StateId.Watch;
        }

        public void StateEnter(AiAgent agent)
        {
            navMeshAgent.ResetPath();
        }

        public void StateUpdate(AiAgent agent)
        {
            GameObject player = _visionSensor.VisibleObjects.Find(go => go.CompareTag("Player"));
            if (!player) return;
            
            // determine vector to the player
            var vectorToPlayer = player.transform.position - body.transform.position;
            // rotate towards
            Vector3 newDirection = Vector3.RotateTowards(body.transform.forward, vectorToPlayer, Time.fixedDeltaTime/turnSpeed, 0.0f);
            body.transform.rotation = Quaternion.LookRotation(newDirection);
        }

        public void StateExit(AiAgent agent)
        {
            
        }
    }
}