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

public class GrabberController : AdvancedFSM
{
    public GameObject[] hidingSpots;
    private int pointIndex = 0;
    private float acceleration;
    private float deceleration = 600f;
    [HideInInspector]
    public NavMeshAgent agent;
    public PathMode pathMode;
    private int pathDirection = 1;
    private float timeToSearch = 1.0f;

    // Start is called before the first frame update
    protected override void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();

        //Start Doing the Finite State Machine
        ConstructFSM();
    }

    //Update each frame
    protected override void FSMUpdate()
    {
        //Check for health
        elapsedTime += Time.deltaTime;
    }

    protected override void FSMFixedUpdate()
    {
        CurrentState.Reason(playerTransform, transform);
        CurrentState.Act(playerTransform, transform);
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

    private IEnumerator TurnToNextPoint()
    {
        agent.acceleration = deceleration;

        agent.isStopped = true;

        yield return new WaitForSeconds(timeToSearch);
        Debug.Log("test");
        agent.isStopped = false;

        float duration = 1.0f;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            Vector3 dir = agent.destination - transform.position;
            dir.y = 0;//This allows the object to only rotate on its y axis
            Quaternion rot = Quaternion.LookRotation(dir);

            transform.rotation = Quaternion.Lerp(transform.rotation, rot, t / duration);
            yield return null;
        }

        agent.acceleration = acceleration;
    }

    public void GoToNextPoint()
    {
        if (pointIndex == hidingSpots.Length)
        {
            if(pathMode == PathMode.Loop)
            {
                pointIndex = 0;
            }
            else if(pathMode == PathMode.BackAndForth)
            {
                pathDirection = -1;
                pointIndex += pathDirection;
            }
        }
        else if (pointIndex == -1)
        {
            pathDirection = 1;
            pointIndex += pathDirection;
        }

        // Set the agent to go to the currently selected destination.
        agent.SetDestination(hidingSpots[pointIndex].GetComponent<Renderer>().bounds.center + hidingSpots[pointIndex].transform.forward);

        StartCoroutine(TurnToNextPoint());

        // Choose the next point in the array as the destination
        pointIndex += pathDirection;
    }
}
