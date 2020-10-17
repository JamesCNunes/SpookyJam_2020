using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumController : MonoBehaviour
{
    public Transform vacuumOrigin;
    public float suctionStrength = 4f;
    public Collider col;

    bool suck = false;
    bool blow = false;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Vacuumable")
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            Debug.Log("Trig");
            Vector3 direction = vacuumOrigin.position - other.transform.position  ;
            rb.useGravity = false; 
            rb.AddForce(direction.normalized * suctionStrength);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Vacuumable")
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            suck = true;
        } else if (Input.GetMouseButtonUp(0))
        {
            suck = false;
        }
    }
}
