using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SingleButton : MonoBehaviour
{
    public UnityEvent WhenHit;

    public AudioSource Click;

    public Animator anim;

    bool isUp = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (isUp)
        {
            Click.Play();
            anim.SetTrigger("TriggerPressDown");
            Debug.Log("Triggered");
            isUp = false;
            WhenHit.Invoke();
        }

    }
}
