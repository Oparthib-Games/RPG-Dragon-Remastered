using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using _scripts._core;
using _scripts._combat;

namespace _scripts._move
{
    public class Mover : MonoBehaviour, I_Action
    {
        NavMeshAgent navMeshAgent;
        Animator animator;

        [SerializeField] float moveSpeed = 6f;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            Update_Animaton();
        }

        public void preMoveToDestination(Vector3 move_destination, float moveSpeed_multiplier)
        {
            GetComponent<ActionScheduler>().action_schedule(this);
            MoveToDestination(move_destination, moveSpeed_multiplier);
        }
        public void MoveToDestination(Vector3 _destination, float moveSpeed_multiplier)
        {
            if (GetComponent<Health>().isDead()) return;        // if I die
            navMeshAgent.speed = moveSpeed * moveSpeed_multiplier;
            navMeshAgent.destination = _destination;
            navMeshAgent.isStopped = false; // this makes the navmesh agent to NOT stop moving
        }
        public void cancel_action()
        {
            if (GetComponent<Health>().isDead()) return;        // if I die
            navMeshAgent.isStopped = true; // this makes the navmesh agent to stop moving
        }






        void Update_Animaton()
        {
            Vector3 navMeshVecolicty = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(navMeshVecolicty); // coverting navmesh global velocity to players local velocity 
            float z_speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", z_speed);
        }


    }

}