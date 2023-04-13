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
            transform.position = PushPullPoint.position;
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        StopPushingPulling();
    }*/
}
