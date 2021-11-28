using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : FSMState
{
    float t = 0;
    // Start is called before the first frame update
    public AttackState()
    {
        stateID = FSMStateID.Attacking;
    }

    public override void Reason(Transform player, Transform npc)
    {
        float dist = Vector3.Distance(npc.position, player.position);

        if (dist >= 2.0f)
        {
            npc.GetComponent<EnemyController>().attackCountdown = 0;
            npc.GetComponent<NavMeshAgent>().acceleration = npc.GetComponent<EnemyController>().acceleration;
            npc.GetComponent<NavMeshAgent>().isStopped = false;
            npc.GetComponent<EnemyController>().PerformTransition(Transition.PlayerEscaped);
        }
    }
    public override void Act(Transform player, Transform npc)
    {
        if(npc.GetComponent<EnemyController>().attackCountdown <= 0)
        {
            Debug.Log("Player attacked!"); //Just debug until health system implemented
            npc.GetComponent<EnemyController>().attackCountdown = npc.GetComponent<EnemyController>().attackCooldownTime;
        }
        else
        {
            npc.GetComponent<EnemyController>().attackCountdown -= Time.deltaTime;
        }
    }
}
