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
    [SerializeField] private float distance = 1;
    private PlayerScript PS;
    private PushablePullable PushablePullable;

    [Header("LayerMask")]
    [SerializeField] private LayerMask PushPull;

    [Header("Push Audio")]
    public AudioSource pushSound;

    public Animator anim;

    void Start()
    {
        GameObject PlayerObject = GameObject.FindWithTag("Player");
        if (PlayerObject != null)
        {
            PS = PlayerObject.GetComponent<PlayerScript>();
            anim = PlayerObject.GetComponent<PlayerScript>().GetAnimator();
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

            if (pushpulling != true)
            {
            return;
            }
            if (PushablePullable == null)
            {
                float PushPullDistance = 1;
                if (Physics.Raycast(PlayerCameraTransform.position, PlayerCameraTransform.forward, out RaycastHit raycastHit, PushPullDistance, PushPull))
                {
                    if (raycastHit.transform.TryGetComponent(out PushablePullable))
                    {
                        StartPushingPullingStone();
                    }
                }
            }
            else 
            {
                StopPushingPullingStone();
            }

          
          if (!PS.charactercontroller.isGrounded)
           {
            playerpushpull.Disable();
           }
          else 
          {
            playerpushpull.Enable();
          }
         }
    private void DisitanceBetweenpoint()
    {
        float distanceBetween = Vector3.Distance (PushPullPoint.transform.position, PushablePullable.PushPullPointInteractable.transform.position); // grabs the player's pushpoint transformation information and the pushablepullable transformation information and calculate's the distance 
        Debug.Log("The distance between them " + distanceBetween + " units");
        if (distanceBetween > distance) 
        {
            StopPushingPullingStone();
        }
    }

    private void StopPushingPullingStone()
    {
        PushablePullable.StopPushingPulling();
        PushablePullable = null;
        PS.speed = PS.speed + pullspeed;
        PS.playerjump.Enable();
        PS.IsPushingPulling = false;

        anim.SetBool("isPushing", false);

        pushSound.Stop();
    }

    private void StartPushingPullingStone()
    {
        PushablePullable.PushPullInteract(PushPullPoint);
        PS.speed = PS.speed - pullspeed;
        PS.playerjump.Disable();
        PS.IsPushingPulling = true;


        anim.SetBool("isPushing", true);
        if (!pushSound.isPlaying)
        {
            pushSound.Play();
        }
    }

}
