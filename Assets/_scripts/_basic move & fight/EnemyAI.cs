using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _scripts._combat;
using _scripts._move;
using _scripts._core;

namespace _scripts._Ctrl
{
    public class EnemyAI : MonoBehaviour
    {
        GameObject player;

        Fighter fighter;


        [SerializeField] float suspicion_time= 3f;
                         float time_since_last_saw_player = Mathf.Infinity;

        [SerializeField] float chase_distance = 5f;
        [SerializeField] float way_point_tolerance = 1f;
        int curr_waypoint_index;

        [SerializeField] float waypoint_dwell_time = 5f;
                         float time_since_arrived_waypoint = Mathf.Infinity;

        [SerializeField] Transform myWaypoint;

        void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            time_since_last_saw_player += Time.deltaTime;
            time_since_arrived_waypoint += Time.deltaTime;

            if (isInChaseRange())
            {
                time_since_last_saw_player = 0;
                fighter.Attack(player);
            }
            else if (time_since_last_saw_player < suspicion_time)
            {
                GetComponent<ActionScheduler>().cancel_both_move_and_fight();   //[-SUSPICION-]
            }
            else
            {
                Patroling();                                                    //[-GUARD-]
            }
        }

        private void Patroling()
        {
            if(isReachedWaypoint())
                curr_waypoint_index = Next_Waypoint_Pos(curr_waypoint_index);
            
            if(time_since_arrived_waypoint > waypoint_dwell_time)
            {
                GetComponent<Mover>().preMoveToDestination(getWaypointPos(curr_waypoint_index), 0.6f);

                time_since_arrived_waypoint = 0;
            }
        }
        bool isReachedWaypoint()
        {
            float distance_to_waypoint = Vector3.Distance(transform.position, getWaypointPos(curr_waypoint_index));
            return distance_to_waypoint <= way_point_tolerance;
        }
        Vector3 getWaypointPos(int i){
            return myWaypoint.GetChild(i).position;
        }
        int Next_Waypoint_Pos(int i){
            if (i + 1 == myWaypoint.childCount) return 0;
            return i + 1;
        }


        private bool isInChaseRange(){
            return Vector3.Distance(transform.position, player.transform.position) < chase_distance;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawWireSphere(transform.position, chase_distance);
        }
    }

}