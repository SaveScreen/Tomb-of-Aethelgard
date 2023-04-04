using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightInteractionScript : MonoBehaviour
{

    [Header("Refrences")]
    private PlayerScript PS;
    public bool Interact;

    [Header("Interactions")]
    

    public float rotationJump = 10.0f;
    public float rotationSpeed = 5f;

    private bool rotCW; //is Rotating Clockwise
    private float targetTotalRot; //in degrees, counts past 360
    private float currentTotalRot = 0f; //in degrees, counts past 360
    private bool currentlyRotating = false;

    //public float tweenValue = 0.9f;

    [Header("Inputs")]
    public KeyCode RotateClockWise = KeyCode.Q;
    public KeyCode RotateCtrClockWise = KeyCode.E;

    private float rotationTarget;

    //HUD to tell players to press E/Q to rotate the pillar
    private GameObject eqImage;

    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerObject = GameObject.FindWithTag("Player");
        
        //HUD to tell players to press E/Q to rotate the pillar
     //   eqImage = GameObject.Find("Rotate UI");
//        eqImage.SetActive(false);


        if (PlayerObject != null)
        {
            PS = PlayerObject.GetComponent<PlayerScript>();
        }
        Interact = false;
      
    }

    void Update()
    {
        //need to add a line here to make this input impossible while paused
        if (Interact == true && !currentlyRotating)
        {
            if (Input.GetKeyDown(RotateClockWise))
            {
                RotateLightClockWise();
            }
            if (Input.GetKeyDown(RotateCtrClockWise))
            {
                RotateLightCtrClockWise();
            }
        }

        if(!Mathf.Approximately(transform.eulerAngles.y,  rotationTarget)){
            currentlyRotating = true;
            RotateTowards(rotationTarget);
        } else{
            currentlyRotating = false;
        }

    //   Debug.Log("current y: " + transform.eulerAngles.y);
    //   Debug.Log("target: " + rotationTarget);
    }
    void OnTriggerEnter(Collider other)
    {
        if (PS != null)
        {
            Interact= true;  
          //  eqImage.SetActive(true);
        }   
    }
    void OnTriggerExit(Collider other)
    {
        if (PS != null)
        {
            Interact= false;
          //  eqImage.SetActive(false);
        }

    }

    private void RotateLightClockWise()
    {
        rotationTarget = transform.eulerAngles.y + (rotationJump);
        
        //constrain euler angles to x >= 0 and x < 360
        if(rotationTarget >= 360){
            rotationTarget -= 360;
        }
        rotCW = true;
        targetTotalRot += rotationJump;
    }

    private void RotateLightCtrClockWise()
    {
        rotationTarget = transform.eulerAngles.y - (rotationJump); 

        //constrain euler angles to x >= 0 and x < 360
        if(rotationTarget < 0){
            rotationTarget += 360;
        }
        rotCW = false;
        targetTotalRot -= rotationJump;
    }

    private void RotateTowards(float endRotation){
        if(rotCW){
            Vector3 eulerA = new Vector3();
            eulerA = transform.eulerAngles;
            eulerA += new Vector3(0f, rotationSpeed * Time.deltaTime, 0f);
            transform.eulerAngles = eulerA;

            //keep track of total rotation
            currentTotalRot += rotationSpeed *Time.deltaTime;

            //adjust for overshooting
            if(currentTotalRot > targetTotalRot){
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, endRotation, transform.eulerAngles.z);
            }
        }
        else{
            Vector3 eulerA = new Vector3();
            eulerA = transform.eulerAngles;
            eulerA -= new Vector3(0f, rotationSpeed * Time.deltaTime, 0f);
            transform.eulerAngles = eulerA;

            //keep track of total rotation
            currentTotalRot -= rotationSpeed *Time.deltaTime;

            //adjust for overshooting
            if(currentTotalRot < targetTotalRot){
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, endRotation, transform.eulerAngles.z);
            }
        }
    }
}
