using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : FSMState
{
    // Start is called before the first frame update
    public ChaseState()
    {
        stateID = FSMStateID.Chasing;
    }

    public override void Reason(Transform player, Transform npc)
    {
        float dist = Vector3.Distance(npc.position, npc.GetComponent<NavMeshAgent>().destination);

        if (dist > 6.0f)
        {
            npc.GetComponent<EnemyController>().GoToNextPoint();
            player.GetComponent<PlayerController>().underAttack = false;
            npc.GetComponent<EnemyController>().PerformTransition(Transition.LostPlayer);
        }
        else if (dist <= 1.5f)
        {
            npc.GetComponent<NavMeshAgent>().acceleration = npc.GetComponent<EnemyController>().deceleration;
            npc.GetComponent<NavMeshAgent>().isStopped = true;
            npc.GetComponent<EnemyController>().PerformTransition(Transition.ReachPlayer);
        }
    }
    public override void Act(Transform player, Transform npc)
    {
        npc.GetComponent<NavMeshAgent>().SetDestination(player.position);
        player.GetComponent<PlayerController>().underAttack = true;
    }
}
