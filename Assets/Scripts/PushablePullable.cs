using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushablePullable : MonoBehaviour
{
    private Rigidbody PushablePullableRigdBody;
    private Transform PushPullPoint;

    private void Awake()
    {
        PushablePullableRigdBody = GetComponent<Rigidbody>();
    }


    public void PushPullInteract(Transform PushPullPoint)
    {
        this.PushPullPoint= PushPullPoint;
        PushablePullableRigdBody.useGravity = false;
    }

    public void StopPushingPulling()
    {
        this.PushPullPoint = null;
        PushablePullableRigdBody.useGravity = true;
    }

    private void FixedUpdate()
    {
        if(PushPullPoint != null) 
        {
            PushablePullableRigdBody.MovePosition(PushPullPoint.position);
        }
    }
}
