using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthScript))]
public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 3.0f;
    private float moveDirectionLR = 0;
    private float moveDirectionUD = 0;
    private Rigidbody rb;
    private Animator animator;
    [HideInInspector]
    public GameObject hidingSpot = null;
    private GameObject targetEnemy = null;
    [HideInInspector]
    public bool hiding = false;
    [HideInInspector]
    public bool underAttack = false;
    public int keys = 0;
    private bool moving;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
                moving = true;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                moveDirectionLR = 1;
                moving = true;
            }
            else
            {
                moveDirectionLR = 0;
                moving = false;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                moveDirectionUD = -1;
                moving = true;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                moveDirectionUD = 1;
                moving = true;
            }
            else
            {
                moveDirectionUD = 0;
                moving = false;
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

        if (Input.GetKeyDown(KeyCode.Q) && targetEnemy != null && !underAttack)
        {
            animator.SetTrigger("Attacking");
            targetEnemy.GetComponent<EnemyController>().Die();
        }

        Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);

        RaycastHit hit;
        Vector3 groundCheckPos = GetComponent<Collider>().bounds.min;
        Physics.Raycast(groundCheckPos, Vector3.down, out hit);
        transform.position = new Vector3(transform.position.x, hit.point.y + 2.5f, transform.position.z);

        UpdateSound();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(moveDirectionLR * maxSpeed, 0, moveDirectionUD * maxSpeed);
        transform.LookAt(transform.position + new Vector3(moveDirectionLR, 0, moveDirectionUD).normalized);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hiding Spot"))
        {
            hidingSpot = other.gameObject;
        }
        else if (other.CompareTag("Enemy"))
        {
            targetEnemy = other.gameObject;
        }

        else if (other.CompareTag("Key"))
        {
            keys = keys + 1;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hiding Spot"))
        {
            hidingSpot = null;
        }
        else if (other.CompareTag("Enemy"))
        {
            targetEnemy = null;
        }
    }

    void UpdateSound()
    {
        if (moving)
        {
            audioSource.mute = false;
        }
        else
        {
            audioSource.mute = true;
        }
    }
}
