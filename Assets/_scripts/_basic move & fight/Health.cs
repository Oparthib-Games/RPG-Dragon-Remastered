using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using _scripts._core;

namespace _scripts._combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100;

        bool im_already_dead;
        public void TakeDamage(float amount)
        {
            health = Mathf.Max((health - amount), 0);   // mathf.max returns max btwn 2 value

            if (health == 0 && !im_already_dead)
            {
                GetComponent<ActionScheduler>().cancel_both_move_and_fight();
                GetComponent<CapsuleCollider>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
                GetComponent<Animator>().SetTrigger("die");
                im_already_dead = true;
            }
        }
        public bool isDead()
        {
            return im_already_dead;
        }
    }

}