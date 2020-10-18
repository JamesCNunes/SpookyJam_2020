using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOrigin : MonoBehaviour
{
    public Transform origin;
    public Rigidbody rb;
    public AudioSource source;

    public float splatSpeed;
    public void ResetPos()
    {
        gameObject.transform.position = origin.position;
        gameObject.transform.rotation = origin.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("P_hit: " + rb.velocity.magnitude);
        if(rb.velocity.magnitude >= splatSpeed)
        {
            source.Play();
        }
    }
}
