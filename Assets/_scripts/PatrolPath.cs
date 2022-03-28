using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _scripts._core
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypoint_radius = 0.3f;
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(getWaypointPos(i), waypoint_radius);

                Gizmos.DrawLine(getWaypointPos(i), getWaypointPos(next_index(i)));
            }
        }

        int next_index(int i)
        {
            if (i + 1 == transform.childCount) return 0;
            return i + 1;
        }

        Vector3 getWaypointPos(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
