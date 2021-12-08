﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 3.0f;
    private float moveDirectionLR = 0;
    private float moveDirectionUD = 0;
    private Rigidbody rb;
    [HideInInspector]
    public GameObject hidingSpot = null;
    [HideInInspector]
    public bool hiding = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hiding)
        {
            // Movement controls
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveDirectionLR = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                moveDirectionLR = 1;
            }
            else
            {
                moveDirectionLR = 0;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                moveDirectionUD = -1;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                moveDirectionUD = 1;
            }
            else
            {
                moveDirectionUD = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (hidingSpot != null)
            {
                if (!hiding)
                {
                    transform.position = hidingSpot.GetComponent<Renderer>().bounds.center;
                }
                else
                {
                    transform.position = hidingSpot.GetComponent<Renderer>().bounds.center + hidingSpot.transform.forward;
                }

                hiding = !hiding;
            }
        }

        Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(moveDirectionLR * maxSpeed, 0, moveDirectionUD * maxSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hiding Spot"))
        {
            hidingSpot = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hiding Spot"))
        {
            hidingSpot = null;
        }
    }
}