using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LightInteractionScript : MonoBehaviour
{

    [Header("Refrences")]
    private PlayerScript PS;
    public AudioSource Audio;
    public AudioClip spinMirrorAudio;

    [Header("Interactions")]    
    public float rotationJump = 10.0f;
    public float rotationSpeed = 5f;
    public bool Interact;
    private bool rotCW; //is Rotating Clockwise
    private float targetTotalRot; //in degrees, counts past 360
    private float currentTotalRot = 0f; //in degrees, counts past 360
    private bool currentlyRotating = false;

    //public float tweenValue = 0.9f;

    [Header("Inputs")]
    public InputAction playerrotating;
    private float playerrotatingval;

    private float rotationTarget;

    //HUD to tell players to press E/Q to rotate the pillar
    private GameObject eqImage;

    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerObject = GameObject.FindWithTag("Player");
        if (PlayerObject != null)
        {
            PS = PlayerObject.GetComponent<PlayerScript>();
        }
            //HUD to tell players to press E/Q to rotate the pillar
            //   eqImage = GameObject.Find("Rotate UI");
            //        eqImage.SetActive(false);

            
      
    }

    void OnEnable() {
        playerrotating.Enable();
    }

    void OnDisable() {
        playerrotating.Disable();
    }

    void Update()
    {
        playerrotatingval = playerrotating.ReadValue<float>();

        //need to add a line here to make this input impossible while paused
        if ((PS.CanInteractRotatingMirror == true) && (!currentlyRotating) && (Interact))
        {
            
            if (playerrotatingval > 0) {
                RotateLightClockWise();
                
            }
            if (playerrotatingval < 0) {
                RotateLightCtrClockWise();
                
            }
        }

        if(!Mathf.Approximately(transform.eulerAngles.y,  rotationTarget)){
            currentlyRotating = true;
            RotateTowards(rotationTarget);
        } else{
            currentlyRotating = false;
        }

   
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Interact = true;
            //  eqImage.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Interact = false;
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
        PlaySpinSound();
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
        PlaySpinSound();
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
    public void PlaySpinSound()
    {
        Audio.PlayOneShot(spinMirrorAudio);
    }
}
