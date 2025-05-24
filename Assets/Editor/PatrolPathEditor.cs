using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using WayPoints;

namespace Editor
{
    [CustomEditor(typeof(PatrolPath))]
    public class PatrolPathEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            PatrolPath path = (PatrolPath)target;
            for (int i = 0; i < path.WayPoints.Count; i++)
            {
                Gizmos.color = Color.cyan;
                Handles.Label(path.WayPoints[i], "WP:" + i);
                path.WayPoints[i] = Handles.PositionHandle(path.WayPoints[i], Quaternion.identity);
            }
        }
    }
}
