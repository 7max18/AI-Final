using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public float health;

    public Slider healthbar;

    private bool isPlayer;

    private void Start()
    {
        if (gameObject.tag == "Player") { isPlayer = true; }
        else { isPlayer = false; }
    }

    public void damage (float attack)
    {
        health -= attack;
        Debug.Log(health);
        if (health <= 0)
        {
            //Trigger the death animation

            if (isPlayer == true)
            {
                //Broadcast a message to the rest of the scene that the player died
                //Reset the scene after a wait (maybe use an IEnumerator?)
            }
            else
            {
                //Destroy the character
            }
        }
    }

    private void Update()
    {
        if (isPlayer == true) { healthbar.value = health; }        
    }
}
