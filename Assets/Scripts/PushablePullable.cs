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
        
    }

    public void StopPushingPulling()
    {
        this.PushPullPoint = null;
     
    }

    private void FixedUpdate()
    {
        if(PushPullPoint != null) 
        {
            transform.position = PushPullPoint.position;
        }
    }
}
