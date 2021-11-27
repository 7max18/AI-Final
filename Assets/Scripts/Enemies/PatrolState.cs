using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : FSMState
{
    // Start is called before the first frame update
    public PatrolState()
    {
        stateID = FSMStateID.Patrolling;
    }

    public override void Reason(Transform player, Transform npc)
    {
        if(npc.GetComponent<Perspective>().enemySpotted && !player.GetComponent<PlayerController>().hiding)
        {
            npc.GetComponent<NavMeshAgent>().acceleration = npc.GetComponent<EnemyController>().deceleration;
            npc.GetComponent<EnemyController>().PerformTransition(Transition.SawPlayer);
        }
    }
    public override void Act(Transform player, Transform npc)
    {
        if (!npc.GetComponent<EnemyController>().agent.pathPending && npc.GetComponent<EnemyController>().agent.remainingDistance <= 0.5f)
        {
            GameObject waypoint = npc.GetComponent<EnemyController>().waypoints[npc.GetComponent<EnemyController>().pointIndex];
            if (waypoint.CompareTag("Hiding Spot") && player.GetComponent<PlayerController>().hidingSpot == waypoint && player.GetComponent<PlayerController>().hiding)
            {
                Debug.Log("Player found!");
            }

            npc.GetComponent<EnemyController>().GoToNextPoint();
        }
    }
}
