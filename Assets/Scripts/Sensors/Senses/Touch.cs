using UnityEngine;
using System.Collections;

public class Touch : Sense
{
    void OnTriggerEnter(Collider other)
    {
        EventParam myparams = default(EventParam);

        Aspect aspect = other.GetComponent<Aspect>();
        if (aspect != null)
        {
            //Check the aspect
            if (aspect.aspectName == aspectName)
            {
                EventManagerDelPara.TriggerEvent("SoundAlert", myparams);
                //piggybacking this code since it's ultimately doing the same thing as the soundtower
                Debug.Log("Enemy Touch Detected");
            }
        }
    }


}
