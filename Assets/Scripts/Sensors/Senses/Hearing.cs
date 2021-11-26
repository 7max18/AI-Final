using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearing : Sense
{
    public int hearingRange = 150;

    private Transform playerTrans;
    private Vector3 rayDirection;

    EventParam myparams = default(EventParam);

    Animator animator;

    protected override void Initialise()
    {
        //set the value for the player transform -- playerTrans is the var name
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        myparams.transformParam = transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void UpdateSense()
    {
        //update time passing in var elapsedTime

        //if enough time has passed [var in Sense.cs], poll for Aspect
        elapsedTime += Time.deltaTime;
        //This line should be wherever the code that controls the tower leaving the detecting state
        //animator.SetBool("TankLocated", false);


        if (elapsedTime >= detectionRate)
        {
            DetectAspect();
            elapsedTime = 0.0f;
        }
    }

    void DetectAspect()
    {
        //var to hold our RaycastHit
        RaycastHit hit;
        rayDirection = playerTrans.position - transform.position; //rayDirection set toward player position

        if (Physics.Raycast(transform.position, rayDirection, out hit, hearingRange))
        {
            //if something is in range, see if it's making noise
            if (hit.collider.gameObject.GetComponent<AudioSource>() && !hit.collider.gameObject.GetComponent<AudioSource>().mute)
            {
                //If it is making noise, see if it's what the AI is looking for
                Aspect aspect = hit.collider.GetComponent<Aspect>();
                if (aspect != null)
                {
                    //Check the aspect
                    if (aspect.aspectName == aspectName)
                    {
                        //now console out -- Enemy Detected!
                        Debug.Log("Enemy Detected");
                        EventManagerDelPara.TriggerEvent("SoundAlert", myparams);
                        animator.SetBool("TankLocated", true);
                    }
                    else
                    {
                        animator.SetBool("TankLocated", false);
                    }

                }
                else
                {
                    animator.SetBool("TankLocated", false);
                }
            }
            else
            {
                animator.SetBool("TankLocated", false);
            }
        }
        else
        {
            animator.SetBool("TankLocated", false);
        }
    } //end detectaspect
}
