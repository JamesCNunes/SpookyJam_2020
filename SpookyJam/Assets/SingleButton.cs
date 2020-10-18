using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SingleButton : MonoBehaviour
{
    public UnityEvent WhenHit;

    bool isUp;
    private void OnCollisionEnter(Collision collision)
    {
        if (isUp)
        {
            isUp = false;
            WhenHit.Invoke();
        }

    }
}
