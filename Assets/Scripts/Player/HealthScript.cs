using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public float health;

    public Slider healthbar;

    private bool isPlayer;

    private Animator animator;

    private void Start()
    {
        if (gameObject.tag == "Player") 
        { 
            isPlayer = true;
            animator = GetComponent<Animator>();
        }
        else { isPlayer = false; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isPlayer == true)
        {
            if (collision.gameObject.CompareTag("Enemy") == true)
            {
                health--;
                animator.SetTrigger("Attacked");
            }
        }
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
                animator.SetBool("IsDead", true);
            }
            else
            {
                //Destroy the Game Object
            }
        }
    }

    private void Update()
    {
        if (isPlayer == true) { healthbar.value = health; }        
    }
}
