using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSwitch : MonoBehaviour
{
    //IMPORTANT!! Remember to set the object that you want to be disabled when the player gets near.
    //You should *only pair a Search Tower or Drone Guard.*
    public GameObject pairedObject;

    private void Start()
    {
        if (pairedObject = null)
        {
            Debug.LogError("Error: Please set an object to disable with the switch!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            pairedObject.SetActive(false);
        }
    }
}
