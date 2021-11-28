using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : AdvancedFSM
{
    public GameObject[] waypoints;
    [HideInInspector]
    public int pointIndex = 0;
    [HideInInspector]
    public float acceleration;
    public float deceleration = 600f;
    [HideInInspector]
    public NavMeshAgent agent;
    public float attackCooldownTime = 0;
    [HideInInspector]
    public float attackCountdown;
    public PathMode pathMode;
    private int pathDirection = 1;
    private float timeToSearch = 1.0f;


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

    private IEnumerator TurnToNextPoint()
    {
        agent.acceleration = deceleration;

        agent.isStopped = true;

        yield return new WaitForSeconds(timeToSearch);

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
        // Set the agent to go to the currently selected destination.
        if(waypoints[pointIndex].CompareTag("Hiding Spot"))
        {
            agent.SetDestination(waypoints[pointIndex].GetComponent<Renderer>().bounds.center + waypoints[pointIndex].transform.forward);
        }
        else
        {
            agent.SetDestination(waypoints[pointIndex].transform.position);
        }

        StartCoroutine(TurnToNextPoint());

        // Choose the next point in the array as the destination
        pointIndex += pathDirection;

        if (pointIndex == waypoints.Length)
        {
            if (pathMode == PathMode.Loop)
            {
                pointIndex = 0;
            }
            else if (pathMode == PathMode.BackAndForth)
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
    }

    public void FaceTarget()
    {
        StartCoroutine(FaceTargetCoroutine());
    }

    private IEnumerator FaceTargetCoroutine()
    {
        agent.acceleration = deceleration;

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
}
