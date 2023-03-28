using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullScript : MonoBehaviour
{
   
    [Header("Interactions")]
    public float pullspeed = 10;

    [Header("Inputs")]
    public KeyCode PushingPulling = KeyCode.R;

    [Header("Refrences")]
    [SerializeField] private Transform PlayerCameraTransform;
    [SerializeField] private Transform PushPullPoint;
    private PlayerScript PS;
    private PushablePullable PushablePullable;

    [Header("LayerMask")]
    [SerializeField] private LayerMask PushPull;


    void Start()
    {
        GameObject PlayerObject = GameObject.FindWithTag("Player");
        if (PlayerObject != null)
        {
            PS = PlayerObject.GetComponent<PlayerScript>();
        }
    }
        // Start is called before the first frame update
        private void Update()
       {
          if (Input.GetKeyDown(PushingPulling))
          {
            if (PushablePullable == null)
            {
                float PushPullDistance = 1;
                if (Physics.Raycast(PlayerCameraTransform.position, PlayerCameraTransform.forward, out RaycastHit raycastHit, PushPullDistance, PushPull))
                {
                    if (raycastHit.transform.TryGetComponent(out PushablePullable))
                    {
                        PushablePullable.PushPullInteract(PushPullPoint);

                        PS.transform.rotation = Quaternion.Euler(0, 0, 0);
                        PS.speed = PS.speed - pullspeed;
                    }
                }
            }
            else 
            {
                PushablePullable.StopPushingPulling();
                PushablePullable = null;
                PS.speed = PS.speed + pullspeed;
            }
          }
       }
}
