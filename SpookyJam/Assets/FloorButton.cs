using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloorButton : MonoBehaviour
{
    public UnityEvent WhenDown;
    public UnityEvent WhenUp;

    void Awake()
    {
        if (WhenDown == null)
            WhenDown = new UnityEvent();
        if (WhenUp == null)
            WhenUp = new UnityEvent();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Test");
    }
}
