using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float health;

    public void damage (float attack)
    {
        health -= attack;
    }
}
