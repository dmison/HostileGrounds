using Pickups;
using UnityEngine;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(Pickup))]
    public class PickupEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            Pickup p = (Pickup)target;
            PickupData data = p?.pickupData;
            if (!data) return;
            Handles.Label(p.transform.position+(Vector3.up/2), data.name);
        }
    }
}
