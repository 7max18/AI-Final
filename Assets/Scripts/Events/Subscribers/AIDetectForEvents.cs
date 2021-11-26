using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AIDetectForEvents : MonoBehaviour
{
    private Action<EventParam> soundAlertListener;
    private Action<EventParam> someListener2;


    void Awake()
    {
        //someListener2 = new Action<EventParam>(SomeOtherFunction);
    }

    
    void OnEnable()
    {
        //Register With Action variable
        EventManagerDelPara.StartListening("SoundAlert", soundAlertListener);

        //OR Register Directly to function
        //EventManagerDelPara.StartListening("boom", SomeOtherFunction);
    }

    void OnDisable()
    {
        //Un-Register With Action variable
        EventManagerDelPara.StopListening("SoundAlert", soundAlertListener);

        //OR Un-Register Directly to function
        //EventManagerDelPara.StopListening("boom", SomeOtherFunction);
    }

    void SomeOtherFunction(EventParam eventParam)
    {
        Debug.Log("Some Other Delegate w Parameters Function was called!");
        if (eventParam.param1 == "joy")
        {
            Debug.Log("test had JOY");
            Debug.Log("and param4 the bool set to default was: " + eventParam.param4);
        }
    }

    void SomeThirdFunction(EventParam eventParam)
    {
        Debug.Log("Some Third Delegate w Parameters Function was called!");
    }
}
