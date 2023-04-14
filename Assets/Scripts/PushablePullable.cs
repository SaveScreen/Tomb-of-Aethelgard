using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushablePullable : MonoBehaviour
{
    private Rigidbody PushablePullableRigdBody;
    public Transform PushPullPointInteractable { get; private set; }
    [Header("speed for pushpull")]
    [SerializeField] private float speed = 10;
    

    private void Awake()
    {
        PushablePullableRigdBody = GetComponent<Rigidbody>();
        PushablePullableRigdBody.isKinematic = true;
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
