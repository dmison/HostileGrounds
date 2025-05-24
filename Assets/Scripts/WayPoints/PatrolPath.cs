using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WayPoints
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField]
        private List<Vector3> wayPoints = new List<Vector3>();
        public List<Vector3> WayPoints { get { return wayPoints; } }
        private void OnDrawGizmos()
        {
            for(int i=0; i<wayPoints.Count; i++) 
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawCube(wayPoints[i], Vector3.one/2);
                
                if (i == 0)
                {
                    Gizmos.DrawLine(wayPoints[wayPoints.Count-1], wayPoints[0]);    
                }
                else
                {
                    Gizmos.DrawLine(wayPoints[i-1], wayPoints[i]);
                }
            }
        }
        
        public (Vector3, int) GetClosestWayPoint(Vector3 origin)
        {
            List<float> distances = wayPoints.Select(wp => Vector3.Distance(origin, wp)).ToList();
            float shortestDistance = distances.Min();
            int index = distances.IndexOf(shortestDistance);

            (Vector3, int) closestWayPoint = (wayPoints[index], index);
            return closestWayPoint;
        }
        
        public (Vector3, int) GetWayPoint(int index)
        {
            var lastIndex = wayPoints.Count - 1;
            if (wayPoints.Count == 0)
            {
                return (Vector3.one, 0);
            }
            
            if ((index < 0) || (index > lastIndex))
            {
                return (wayPoints[0], 0);
            }
            return (wayPoints[index], index);
        }
        
        public (Vector3, int) GetNextWayPoint(int index)
        {
            index++;
            return GetWayPoint(index);
        }
        
        
    }
}
