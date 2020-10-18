using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOrigin : MonoBehaviour
{
    public Transform origin;

    public void ResetPos()
    {
        gameObject.transform.position = origin.position;
        gameObject.transform.rotation = origin.rotation;
    }
}
