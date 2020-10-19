using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VacuumController : MonoBehaviour
{
    public Transform vacuumOrigin;
    public Transform holdPoint;
    public float suctionStrength = 4f;
    public float launchStrength = 10f;
    public float holdDist = 0.5f;
    public Collider col;
    GameObject heldObject;

    public AudioSource main;
    public AudioSource winMusic;
    public AudioSource VacSFX;
    public AudioSource Fwoosh;

    public AudioClip blowSound;
    public AudioClip suckSound;

    public GameObject BlowPFX;
    public GameObject SuckPFX;


    bool suckEnabled = false;
    bool blowEnabled = false;
    bool holding = false;
    bool launchable = true;
    bool endGame = false;

    public float cooldownTime = 1f;
    float cooldownLaunch;

    private void Start()
    {
        cooldownLaunch = cooldownTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if((other.tag == "Vacuumable" || other.tag == "Goal") && launchable)
        {
            Debug.Log("Trig");
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            //if sucking
            if (suckEnabled)
            {
               // Debug.Log("Sucking");
                float dist = Vector3.Distance(holdPoint.position, other.transform.position);

                if (dist <= holdDist && !holding && other.tag != "Goal")
                {
                    //Debug.Log("Now Held");
                    holding = true;
                    rb.useGravity = false;
                    other.transform.position = holdPoint.position;
                    other.transform.parent = holdPoint;
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    heldObject = other.gameObject;
                    rb.isKinematic = true;
                    //  heldObject.GetComponent<Collider>().enabled = false;
                    return;
                } else if (other.tag == "Goal" && dist <= holdDist && !holding)
                {
                    Destroy(other.gameObject);
                    Debug.Log("Woohoo!");
                    //play sfx
                    main.mute = true;
                    winMusic.Play();
                    endGame = true;
                    //wait for music to end
                    //transition to next level
                }
                else if (holding)
                {
                    return;
                }

                Vector3 direction = vacuumOrigin.position - other.transform.position;
                rb.useGravity = false;
                rb.AddForce(direction.normalized * suctionStrength);
            }
            else if (blowEnabled)
            {
                //Debug.Log("Blowing");
                
                Vector3 direction = other.transform.position - vacuumOrigin.position;
                rb.useGravity = false;
                rb.isKinematic = false;
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
        if (endGame == true && !winMusic.isPlaying)
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }

        if (!launchable && cooldownLaunch > 0)
        {
            cooldownLaunch -= Time.deltaTime;
            return;
        } else if (!launchable && cooldownLaunch <= 0)
        {
            launchable = true;
            //Debug.Log("Launch Enable");
            cooldownLaunch = cooldownTime;
        }

        if (Input.GetMouseButton(0) && !blowEnabled)
        {
            
            suckEnabled = true;
            if (!VacSFX.isPlaying)
            {
                Debug.Log("sound change");
                VacSFX.clip = suckSound;
                VacSFX.Play();
            }
            
            SuckPFX.SetActive(true);
        } else if (Input.GetMouseButtonUp(0) && !blowEnabled)
        {
            
            suckEnabled = false;

            if (VacSFX.isPlaying)
            {
                Debug.Log("sound off");
                VacSFX.Pause();
            }
            
            SuckPFX.SetActive(false);

            if (holding)
            {
                holding = false;
                heldObject.transform.parent = null;
                heldObject.GetComponent<Rigidbody>().useGravity = true;
                heldObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

        if (Input.GetMouseButton(1) && !suckEnabled)
        {
            BlowPFX.SetActive(true);

            if (!VacSFX.isPlaying)
            {
                Debug.Log("sound change");
                VacSFX.clip = blowSound;
                VacSFX.Play();
                blowEnabled = true;
            }
            
        } 
        else if (Input.GetMouseButton(1) && suckEnabled && holding)
        {
            blowEnabled = false;
            suckEnabled = false;
            SuckPFX.SetActive(false);
            BlowPFX.SetActive(false);
            if (VacSFX.isPlaying)
            {
                Debug.Log("sound pause");
                VacSFX.Pause();
            }

            Fwoosh.Play();
            holding = false;
            launchable = false;
            heldObject.transform.parent = null;
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.GetComponent<Rigidbody>().AddForce((holdPoint.position - vacuumOrigin.position) * launchStrength, ForceMode.Impulse);
        } 
        else if (Input.GetMouseButtonUp(1) && !suckEnabled)
        {
            BlowPFX.SetActive(false);
            if (VacSFX.isPlaying)
            {
                Debug.Log("sound pause");
                VacSFX.Pause();
            }
            
            blowEnabled = false;
            
        }
    }
}
