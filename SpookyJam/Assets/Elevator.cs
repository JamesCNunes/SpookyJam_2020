using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform position1, position2, elevator;
    public float speed;

    bool movingForward, movingBackward, resting = true;




    public void moveForward()
    {
        resting = false;
        movingBackward = false;
        movingForward = true;

    }

    public void moveBackward()
    {
        resting = false;
        movingForward = false;
        movingBackward = true;
        

    }

    private void FixedUpdate()
    {
       /* elevator.localPosition = Vector3.Lerp(elevator.localPosition, position2.localPosition, speed * Time.deltaTime);
        if(elevator.localPosition == position2.localPosition)
        {
            Debug.Log("Rawr");
        }
        */
        
        if (resting)
        {
           // Debug.Log("Resting");
            return;
        }

        if (movingForward)
        {
            //Debug.Log("Forward");
            //Debug.Log("Elev: " + elevator.localPosition + " To Pos: " + position2.localPosition);
            if (elevator.localPosition == position2.localPosition)
            {
                resting = true;
                Debug.Log("Finished Forward");
                return;
            }

            elevator.localPosition = Vector3.Lerp(elevator.localPosition, position2.localPosition, speed * Time.deltaTime);
            //Debug.Log(velocity);
            //Debug.Log(elevator.position);
            //elevator.GetComponent<Rigidbody>().MovePosition(velocity);

        }
        else if (movingBackward)
        {
            //Debug.Log("Backward");
            if (position1.localPosition == elevator.localPosition)
            {
                resting = true;
                return;
            }

            elevator.localPosition = Vector3.Lerp(elevator.localPosition, position1.localPosition, speed * Time.deltaTime);
        }
    }
}
