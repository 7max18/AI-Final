using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject count;
    public float keyChance = 0.2f;
    public float currentkeyChance;
    public int enemies;
    public int curenemies;

    private void Start()
    {
       enemies = transform.childCount;
    }

    private void Update()
    {
        curenemies = transform.childCount;
    }

    void Probability()
    {
        if(enemies < curenemies)
        {
            currentkeyChance = keyChance + 0.2f;
            enemies = curenemies;
            keyChance = currentkeyChance;
        }

        if(enemies == 1)
        {
            currentkeyChance = 1.0f;
        }
    }
      

}
