using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _scripts._core
{
    public class ActionScheduler : MonoBehaviour
    {
        I_Action current_action;


//      # When movement calls, cancel attack 
//      # when attack calls, cancel movement
        public void action_schedule(I_Action action)
        {
            if (current_action == action) return;
                                                                       
            if(current_action != null)
                current_action.cancel_action();

            current_action = action;
        }

        public void cancel_both_move_and_fight()
        {
            action_schedule(null);
        }
    }
}