using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloorButton : MonoBehaviour
{
    public UnityEvent WhenDown;
    public UnityEvent WhenUp;

    bool isUp = true;

    void Awake()
    {
        if (WhenDown == null)
            WhenDown = new UnityEvent();
        if (WhenUp == null)
            WhenUp = new UnityEvent();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isUp)
        {
            Debug.Log("Activated");
            isUp = false;
            WhenDown.Invoke();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isUp)
        {
            isUp = true;
            WhenUp.Invoke();
        }
    }

}
