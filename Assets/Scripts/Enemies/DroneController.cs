using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneController : EnemyController
{
    protected override void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        acceleration = agent.acceleration;

        //Start Doing the Finite State Machine
        ConstructFSM();
    }

    public void SetTransition(Transition t)
    {
        PerformTransition(t);
    }

    private void ConstructFSM()
    {
        PatrolState patrol = new PatrolState();

        AddFSMState(patrol);
    }
}
