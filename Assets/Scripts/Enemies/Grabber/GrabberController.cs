using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PathMode
{
    Static,
    BackAndForth,
    Loop,
}

public class GrabberController : EnemyController
{
    // Start is called before the first frame update
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
        patrol.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);

        ChaseState chase = new ChaseState();
        chase.AddTransition(Transition.LostPlayer, FSMStateID.Patrolling);
        chase.AddTransition(Transition.ReachPlayer, FSMStateID.Attacking);

        AttackState attack = new AttackState();
        attack.AddTransition(Transition.PlayerEscaped, FSMStateID.Chasing);

        AddFSMState(patrol);
        AddFSMState(chase);
        AddFSMState(attack);
    }

    
}
