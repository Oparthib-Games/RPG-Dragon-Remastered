using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _scripts._move;
using System;
using _scripts._core;

namespace _scripts._combat
{
    public class Fighter : MonoBehaviour, I_Action
    {
        [SerializeField] Transform target;

        [SerializeField] float stpRunStrtFightDistance = 5f;
        [SerializeField] float weaponDamage = 5f;

        public float time_btwn_attack;
               float time_since_last_attack = Mathf.Infinity;

        void Update()
        {
            time_since_last_attack += Time.deltaTime;

            if (!target) return;
            if (target.GetComponent<Health>().isDead()) return; // if target dies


            if (!isInRange())
                GetComponent<Mover>().MoveToDestination(target.position, 1);
            else{
                Perform_Attack();
                GetComponent<Mover>().cancel_action();
            }
        }

        private void Perform_Attack(){
            transform.LookAt(target);
            if(time_since_last_attack > time_btwn_attack){
                time_since_last_attack = 0;
                GetComponent<Animator>().SetTrigger("doAttack");
            }
        }

        public void Attack(GameObject passed_target){
            GetComponent<ActionScheduler>().action_schedule(this);
            if (!target)
                target = passed_target.transform;
        }

        private bool isInRange(){
            return Vector3.Distance(transform.position, target.position) < stpRunStrtFightDistance;
        }

        public void cancel_action(){
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
            GetComponent<Mover>().cancel_action();
        }










        void Hit(){// [-ANIMATION EVENT-]
            target.GetComponent<Health>().TakeDamage(weaponDamage);
        }
    }
}