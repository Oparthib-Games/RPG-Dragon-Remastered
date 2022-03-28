using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Cinematics : MonoBehaviour
{
    bool alreadyTriggered;

    public void OnTriggerEnter(Collider other)
    {
        if(!alreadyTriggered && other.tag == "Player")
        {
            GetComponent<PlayableDirector>().Play();
            alreadyTriggered = true;
        }
    }
}
