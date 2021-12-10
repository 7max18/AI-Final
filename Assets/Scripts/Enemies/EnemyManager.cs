using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemiesWithKey = new List<GameObject>();
    public GameObject keyPrefab;
    private bool keyDropped;
    public float keyChance;

    private Action<EventParam> DeathListener;

    void Awake()
    {
        DeathListener = new Action<EventParam>(CheckForKey);
    }

    void OnEnable()
    {
        //Register With Action variable
        EventManagerDelPara.StartListening("Death", DeathListener);
    }

    void OnDisable()
    {
        //Un-Register With Action variable
        EventManagerDelPara.StopListening("Death", DeathListener);
    }

    private void Start()
    {
        keyChance = 1.0f / enemiesWithKey.Count;
    }

    void CheckForKey(EventParam dyingEnemy)
    {
        if (enemiesWithKey.Contains(dyingEnemy.gameObjectParam) && !keyDropped)
        {
            float randValue = UnityEngine.Random.value;

            if (randValue <= keyChance)
            {
                Instantiate(keyPrefab, dyingEnemy.gameObjectParam.transform.position, dyingEnemy.gameObjectParam.transform.rotation);
                keyDropped = true;
            }

            enemiesWithKey.Remove(dyingEnemy.gameObjectParam);
        }
    }
      

}
