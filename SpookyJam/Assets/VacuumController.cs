using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumController : MonoBehaviour
{
    public Transform vacuumOrigin;
    public float suctionStrength = 4f;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Vacuumable"  && Input.GetMouseButton(0))
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
}
