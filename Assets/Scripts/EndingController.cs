using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingController : MonoBehaviour
{
    public float EnemyCount;
    public GameObject Player;

    public HealthScript PlayerHealth;

    public Text EndingText;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerHealth = Player.GetComponent<HealthScript>();
        EndingText.gameObject.SetActive(false);
        EndingText.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth.health <= 0)
        {
            EndingText.gameObject.SetActive(true);
            EndingText.text = "You lose! Try Again?";
        }
        else if (EnemyCount <= 0)
        {
            EndingText.gameObject.SetActive(true);
            EndingText.text = "You win! Congradulations!";
        }
    }
}
