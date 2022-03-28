using _scripts._core;
using _scripts._Ctrl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicCtrlRemover : MonoBehaviour
{
    GameObject Player;
    void Start()
    {
        GetComponent<PlayableDirector>().played += DisableCtrl;
        GetComponent<PlayableDirector>().stopped += EnableCtrl;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void DisableCtrl(PlayableDirector X)
    {
        print("...Cutscene has started");
        Player.GetComponent<ActionScheduler>().cancel_both_move_and_fight();
        Player.GetComponent<PlayerCtrl>().enabled = false;
    }
    void EnableCtrl(PlayableDirector X)
    {
        print("...Cutscene has ended");
        Player.GetComponent<PlayerCtrl>().enabled = true;
    }
}
