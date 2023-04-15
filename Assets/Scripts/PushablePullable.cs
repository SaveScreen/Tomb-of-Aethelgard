using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushablePullable : MonoBehaviour
{
    public Rigidbody PushablePullableRigdBody { get; private set; }
    private Transform PushPullPointInteractable;
    private PushPullScript PPS;
    [Header("speed for pushpull")]
    [SerializeField] private float speed = 10;
    [SerializeField] private float distance = 1;

    private void Awake()
    {
        PushablePullableRigdBody = GetComponent<Rigidbody>();
        PushablePullableRigdBody.isKinematic = true;
    }
    void Start()
    {
        GameObject PlayerObject = GameObject.FindWithTag("Player");
        if (PlayerObject != null)
        {
            PPS = PlayerObject.GetComponent<PushPullScript>();
        }

    }

    public void PushPullInteract(Transform PushPullPoint)
    {
        this.PushPullPointInteractable= PushPullPoint;
        PushablePullableRigdBody.isKinematic = false;
    }

    public void StopPushingPulling()
    {
        this.PushPullPointInteractable = null;
        PushablePullableRigdBody.isKinematic = true;
    }

    private void Update()
    {
        if (PushPullPointInteractable != null)
        {
            float distanceBetween = Vector3.Distance(PPS.PushPullPoint.transform.position, PushablePullableRigdBody.transform.position);
            Debug.Log("The distance between them " + distanceBetween + " units");
            if (distanceBetween > distance) 
            { 
                PPS.StopPushingPullingStone();
            }
        }
    }

    private void FixedUpdate()
    {
        if(PushPullPointInteractable != null) 
        {
            Vector3 direction = (PushPullPointInteractable.position - PushablePullableRigdBody.position);
            //PushablePullableRigdBody.MovePosition(PushPullPoint.position);
            PushablePullableRigdBody.velocity = direction * speed;

        }
    }
   
}
