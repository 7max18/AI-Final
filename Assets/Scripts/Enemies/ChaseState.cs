using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : FSMState
{
    // Start is called before the first frame update
    public ChaseState()
    {
        stateID = FSMStateID.Chasing;
    }

    public override void Reason(Transform player, Transform npc)
    {
        float dist = Vector3.Distance(npc.position, npc.GetComponent<EnemyController>().agent.destination);
        Debug.Log(dist);

        if (dist > 6.0f)
        {
            npc.GetComponent<EnemyController>().GoToNextPoint();
            npc.GetComponent<EnemyController>().PerformTransition(Transition.LostPlayer);
        }
        else if (dist < 0.5f)
        {
            //Attack; just debug for now
            Debug.Log("Attacked!");
        }
    }
    public override void Act(Transform player, Transform npc)
    {
        npc.GetComponent<EnemyController>().agent.SetDestination(player.position);
    }
}
