using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : FSMState
{
    private bool justGotHere = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    public override void Reason(Transform player, Transform npc)
    {
        
    }
    public override void Act(Transform player, Transform npc)
    {
        if (!npc.GetComponent<GrabberController>().agent.pathPending && npc.GetComponent<GrabberController>().agent.remainingDistance <= 0.5f)
        {
            npc.GetComponent<GrabberController>().GoToNextPoint();
        }
    }

    
}
