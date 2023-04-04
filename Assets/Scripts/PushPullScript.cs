using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PushPullScript : MonoBehaviour
{
   
    [Header("Interactions")]
    public float pullspeed = 10;

    [Header("Inputs")]
    public InputAction playerpushpull;
    private bool pushpulling;
    //public KeyCode PushingPulling = KeyCode.R;

    [Header("Refrences")]
    [SerializeField] private Transform PlayerCameraTransform;
    [SerializeField] private Transform PushPullPoint;
    private PlayerScript PS;
    private PushablePullable PushablePullable;

    [Header("LayerMask")]
    [SerializeField] private LayerMask PushPull;

    [Header("Push Audio")]
    public AudioSource pushSound;

    void Start()
    {
        GameObject PlayerObject = GameObject.FindWithTag("Player");
        if (PlayerObject != null)
        {
            PS = PlayerObject.GetComponent<PlayerScript>();
        }
    }

    void OnEnable() {
        playerpushpull.Enable();
    }

    void OnDisable() {
        playerpushpull.Disable();
    }

    
        private void Update()
       {
            pushpulling = playerpushpull.WasPressedThisFrame();
            
          if (pushpulling == true)
          {
            if (PushablePullable == null)
            {
                float PushPullDistance = 1;
                if (Physics.Raycast(PlayerCameraTransform.position, PlayerCameraTransform.forward, out RaycastHit raycastHit, PushPullDistance, PushPull))
                {
                    if (raycastHit.transform.TryGetComponent(out PushablePullable))
                    {
                        PushablePullable.PushPullInteract(PushPullPoint);                   
                        PS.speed = PS.speed - pullspeed;
                        PS.playerjump.Disable();
                        PS.IsPushingPulling = true;

                        if(!pushSound.isPlaying){
                            pushSound.Play();
                        }
                    }
                }
            }
            else 
            {
                PushablePullable.StopPushingPulling();
                PushablePullable = null;
                PS.speed = PS.speed + pullspeed;
                PS.playerjump.Enable();
                PS.IsPushingPulling = false;

                pushSound.Stop();
            }
          }
          if (!PS.charactercontroller.isGrounded)
           {
            playerpushpull.Disable();
           }
          else 
          {
            playerpushpull.Enable();
          }
          /*else
          {
                PushablePullable.StopPushingPulling();
                PushablePullable = null;
                PS.speed = PS.speed + pullspeed;
                PS.playerjump.Enable();
                PS.IsPushingPulling = false;

                pushSound.Stop();
         }*/
       }
}
