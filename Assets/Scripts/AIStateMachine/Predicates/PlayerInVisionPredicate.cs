using AISensors;

namespace AIStateMachine.Predicates
{
    public class PlayerInVisionPredicate: BasePredicate
    {
        private VisionSensor _visionSensor;
        
        private void Start()
        {
            _visionSensor = GetComponent<VisionSensor>();
        }

        public override bool Evaluate()
        {
            return _visionSensor.VisibleObjects.Exists(go => go.CompareTag("Player"));
        }
    }
}