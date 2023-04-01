using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteractionScript : MonoBehaviour
{

    [Header("Refrences")]
    private PlayerScript PS;
    public bool Interact;

    [Header("Interactions")]
    

    public float rotationJump = 10.0f;
    public float rotationSpeed = 5f;

    //public float tweenValue = 0.9f;

    [Header("Inputs")]
    public KeyCode RotateClockWise = KeyCode.Q;
    public KeyCode RotateCtrClockWise = KeyCode.E;

    private float rotationTarget;

    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerObject = GameObject.FindWithTag("Player");
        if (PlayerObject != null)
        {
            PS = PlayerObject.GetComponent<PlayerScript>();
        }
        Interact = false;
      
    }

    void Update()
    {
        if (Interact == true)
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

        if(!Mathf.Approximately(transform.rotation.y,  rotationTarget)){
            RotateTowards(rotationTarget);
        }

       /*Debug.Log("current y: " + transform.rotation.y);
       Debug.Log("target: " + rotationTarget);*/
    }
        void OnTriggerEnter(Collider other)
        {
          if (PS != null)
          {
           Interact= true;  
          }   
        }
    void OnTriggerExit(Collider other)
    {
        if (PS != null)
        {
            Interact= false;
        }

    }

    private void RotateLightClockWise()
    {
        rotationTarget = transform.rotation.y + rotationJump;
    }

    private void RotateLightCtrClockWise()
    {
        rotationTarget = transform.rotation.y - rotationJump;
    }

    private void RotateTowards(float endRotation){
        transform.Rotate(0,rotationSpeed*Time.deltaTime, 0);
    }
}
