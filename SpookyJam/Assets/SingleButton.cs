using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SingleButton : MonoBehaviour
{
    public UnityEvent WhenHit;

    bool isUp = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (isUp)
        {
            Debug.Log("Triggered");
            isUp = false;
            WhenHit.Invoke();
        }

    }
}
