using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumController : MonoBehaviour
{
    public Transform vacuumOrigin;
    public Transform holdPoint;
    public float suctionStrength = 4f;
    public float launchStrength = 10f;
    public float holdDist = 0.5f;
    public Collider col;
    GameObject heldObject;

    bool suckEnabled = false;
    bool blowEnabled = false;
    bool holding = false;
    bool launchable = true;

    public float cooldownTime = 1f;
    float cooldownLaunch;

    private void Start()
    {
        cooldownLaunch = cooldownTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Vacuumable" && launchable)
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            //if sucking
            if (suckEnabled)
            {
                Debug.Log("Sucking");
                float dist = Vector3.Distance(holdPoint.position, other.transform.position);

                if (dist <= holdDist && !holding)
                {
                    Debug.Log("Now Held");
                    holding = true;
                    rb.useGravity = false;
                    other.transform.position = holdPoint.position;
                    other.transform.parent = holdPoint;
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    heldObject = other.gameObject;
                    return;
                } else if (holding)
                {
                    return;
                }

                Vector3 direction = vacuumOrigin.position - other.transform.position;
                rb.useGravity = false;
                rb.AddForce(direction.normalized * suctionStrength);
            }
            else if (blowEnabled)
            {
                Debug.Log("Blowing");
                Vector3 direction = other.transform.position - vacuumOrigin.position;
                rb.useGravity = false;
                rb.AddForce(direction.normalized * suctionStrength);
            }
            else {
                rb.useGravity = true;
            }
            //if blowing
            
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
        if (!launchable && cooldownLaunch > 0)
        {
            cooldownLaunch -= Time.deltaTime;
            return;
        } else if (!launchable && cooldownLaunch <= 0)
        {
            launchable = true;
            Debug.Log("Launch Enable");
            cooldownLaunch = cooldownTime;
        }

        if (Input.GetMouseButton(0) && !blowEnabled)
        {
            suckEnabled = true;
        } else if (Input.GetMouseButtonUp(0) && !blowEnabled)
        {
            suckEnabled = false;

            if (holding)
            {
                holding = false;
                heldObject.transform.parent = null;
                heldObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }

        if (Input.GetMouseButton(1) && !suckEnabled)
        {
            blowEnabled = true;
        } 
        else if (Input.GetMouseButton(1) && suckEnabled && holding)
        {
            blowEnabled = false;
            suckEnabled = false;
            holding = false;
            launchable = false;
            heldObject.transform.parent = null;
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            heldObject.GetComponent<Rigidbody>().AddForce((holdPoint.position - vacuumOrigin.position) * launchStrength, ForceMode.Impulse);
        } 
        else if (Input.GetMouseButtonUp(1) && !suckEnabled)
        {
            blowEnabled = false;
        }
    }
}
