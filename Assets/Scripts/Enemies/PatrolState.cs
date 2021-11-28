using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : FSMState
{
    private int waypointIndex = 0;
    // Start is called before the first frame update
    public PatrolState()
    {
        stateID = FSMStateID.Patrolling;
    }

    public override void Reason(Transform player, Transform npc)
    {
        if(npc.GetComponent<Perspective>().enemySpotted && !player.GetComponent<PlayerController>().hiding)
        {
            npc.GetComponent<EnemyController>().FaceTarget();
            npc.GetComponent<EnemyController>().PerformTransition(Transition.SawPlayer);
        }
    }
    public override void Act(Transform player, Transform npc)
    {
        if (!npc.GetComponent<NavMeshAgent>().pathPending && npc.GetComponent<NavMeshAgent>().remainingDistance <= 0.5f)
        {
            GameObject waypoint = npc.GetComponent<EnemyController>().waypoints[waypointIndex];
            if (waypoint.CompareTag("Hiding Spot") && player.GetComponent<PlayerController>().hidingSpot == waypoint && player.GetComponent<PlayerController>().hiding)
            {
                Debug.Log("Player found!");
                //Attack (different from attack state?)
            }

            waypointIndex = npc.GetComponent<EnemyController>().pointIndex;
            npc.GetComponent<EnemyController>().GoToNextPoint();
        }
    }
}
