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
        PushablePullableRigdBody.isKinematic = false;
        PushablePullableRigdBody.useGravity = true;
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
        PushablePullableRigdBody.useGravity = false;
    }

    public void StopPushingPulling()
    {
        this.PushPullPointInteractable = null;
        PushablePullableRigdBody.useGravity = true;
        PushablePullableRigdBody.isKinematic = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PushablePullableRigdBody.isKinematic = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PushablePullableRigdBody.isKinematic = false;
        }
    }

    private void Update()
        {
            if (PushPullPointInteractable != null)
            {
                float distanceBetween = Vector3.Distance(PPS.PushPullPoint.transform.position, PushablePullableRigdBody.transform.position);
               
            PushablePullableRigdBody.isKinematic = false;
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
