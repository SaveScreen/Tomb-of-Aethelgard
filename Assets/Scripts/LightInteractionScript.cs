using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteractionScript : MonoBehaviour
{

    [Header("Refrences")]
    private PlayerScript PS;
    public bool Interact;

    [Header("Interactions")]
    

    public float rotationSpeed = 10.0f;

    

    [Header("Inputs")]
    public KeyCode RotateClockWise = KeyCode.Q;
    public KeyCode RotateCtrClockWise = KeyCode.E;
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
        transform.Rotate(0,rotationSpeed, 0);
    }

    private void RotateLightCtrClockWise()
    {
        transform.Rotate(0, -rotationSpeed, 0);
    }
}
