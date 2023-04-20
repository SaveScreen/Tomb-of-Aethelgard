using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPushPull : MonoBehaviour
{
    [Header("Interactions")]
    public float pullspeed = 10;

    [Header("Inputs")]
    public InputAction playerpushpull;
    private bool pushpulling;


    [Header("Refrences")]
    [SerializeField] private Transform PlayerCameraTransform;
    private PlayerScript PS;
    GameObject Interactable;

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


    void OnEnable()
    {
        playerpushpull.Enable();
    }

    void OnDisable()
    {
        playerpushpull.Disable();
    }


    private void Update()
    {
        pushpulling = playerpushpull.WasPressedThisFrame();

        if (pushpulling != true)
        {
            return;
        }
        float PushPullDistance = 1;
        if (Physics.Raycast(PlayerCameraTransform.position, PlayerCameraTransform.forward, out RaycastHit raycastHit, PushPullDistance, PushPull))
        {
            if (raycastHit.collider != null) 
            {
                Debug.Log("push");
            }
        }
    }
}
