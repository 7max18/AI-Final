using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float keyChance;
    public float currentkeyChance;
    public int enemies;
    public int curenemies;

    private void Start()
    {
        if(enemies > 1)
        {
          keyChance = 0.2f;
        }
        else if(enemies == 1)
        {
            keyChance = 1.0f;
        }
       enemies = transform.childCount;
        currentkeyChance = keyChance;
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
    }
      

}
