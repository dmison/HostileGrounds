using System;
using AISensors;
using UnityEngine;

namespace AIStateMachine.Predicates
{
    public class NearPlayerPredicate: BasePredicate
    {
        private PlayerProximityDetectorSensor _playerProximityDetectorSensor;
        
        private void Start()
        {
            _playerProximityDetectorSensor = GetComponent<PlayerProximityDetectorSensor>();
        }

        public override bool Evaluate()
        {   
            return _playerProximityDetectorSensor.NearPlayer;
        }
    }
}