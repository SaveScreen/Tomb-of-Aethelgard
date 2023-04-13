using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushablePullable : MonoBehaviour
{
    private Rigidbody PushablePullableRigdBody;
    private Transform PushPullPoint;
    [Header("speed for pushpull")]
    [SerializeField] private float speed = 10;

    private void Awake()
    {
        PushablePullableRigdBody = GetComponent<Rigidbody>();
        PushablePullableRigdBody.isKinematic = true;
    }


    public void PushPullInteract(Transform PushPullPoint)
    {
        this.PushPullPoint= PushPullPoint;
        PushablePullableRigdBody.isKinematic = false;
    }

    public void StopPushingPulling()
    {
        this.PushPullPoint = null;
        PushablePullableRigdBody.isKinematic = true;
    }

    private void FixedUpdate()
    {
        if(PushPullPoint != null) 
        {
            Vector3 direction = (PushPullPoint.position - PushablePullableRigdBody.position);
            //PushablePullableRigdBody.MovePosition(PushPullPoint.position);
            PushablePullableRigdBody.velocity = direction * speed;

        }
    }

  
}
