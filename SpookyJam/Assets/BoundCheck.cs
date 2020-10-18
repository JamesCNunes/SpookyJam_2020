using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Vacuumable")
        {
            //Get spawn origin
            other.GetComponent<SpawnOrigin>().ResetPos();
            //Set obj to spawn origin
        }
    }
}
