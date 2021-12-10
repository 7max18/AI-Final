using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningDoors : MonoBehaviour
{
    public Animator anim;

    public int key;
    void Start()
    {
        anim.GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player"))
        {
        key = other.gameObject.GetComponent<PlayerController>().keys;
        if (key >= 1)
            {
                anim.Play("Door Open");
            }

        if (key >= 2)
            {
                anim.Play("Door Open 2");
            }
        }
    }
}
