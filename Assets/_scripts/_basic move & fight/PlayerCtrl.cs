using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _scripts._move;
using _scripts._combat;
using System.Reflection;

namespace _scripts._Ctrl
{
    public class PlayerCtrl : MonoBehaviour
    {
        void Update()
        {
            if (Interact_With_Combat()) return; //if mouse over target. no need to call the movement.
            if (Interact_With_Click_Move()) return;
            //print("no move no combat"); // if no Interaction then this
        }

        bool Interact_With_Combat()
        {
            RaycastHit[] hitInfo_Ary = Physics.RaycastAll(GetRay());
            foreach(RaycastHit the_hitInfo in hitInfo_Ary)
            {
                CombatTarget combatTarget = the_hitInfo.transform.GetComponent<CombatTarget>();

                if (!combatTarget) continue;

                if(Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(combatTarget.transform.gameObject);
                }
                return true; // target over mouse found.
            }
            return false; // no target over mouse.
        }

        bool Interact_With_Click_Move()
        {
            Ray ray = GetRay();
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().preMoveToDestination(hitInfo.point, 1);
                }
                return true;
            }
            return false;
        }

        private static Ray GetRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}