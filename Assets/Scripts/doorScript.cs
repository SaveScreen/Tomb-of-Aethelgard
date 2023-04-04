using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class doorScript : MonoBehaviour
{
    public GameObject closedPosition;
    public GameObject openPosition;

    public bool startsOpen;
    private bool shouldBeOpen;
    public float openSpeed = 2;
    public float closeSpeed = 1;


    // [Header("Tween value | 0.0-1.0")]
    //public float tweenValue = 1; 
    //1 means no tweening. 0 is instantaneous movement. 
    //Caden recommends 0.3

    /********refrence for screen shake***********/
    private CinemachineImpulseSource impulseSource;

    public AudioSource doorMoveAudio;
    private bool audioIsPlaying = false;

    public int doorID;

    void Start()
    {
        shouldBeOpen = startsOpen;
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {

        if (shouldBeOpen == true)
        {
        
            if (transform.position != openPosition.transform.position)
            {
                Translate(openPosition.transform.position, openSpeed);
//                ScreenShakeManager.instance.CameraShake(impulseSource);
            }
            else
            {
                //rest
                audioIsPlaying = false;
                doorMoveAudio.Stop();
            }
        } else if(shouldBeOpen != true)
        {
            if (transform.position != closedPosition.transform.position)
            {
                Translate(closedPosition.transform.position, closeSpeed);
            //    ScreenShakeManager.instance.CameraShake(impulseSource);
            }
            else
            {
                //rest
                audioIsPlaying = false;
                doorMoveAudio.Stop();
            }
        }
    }
    
    //private Vector3 vel = Vector3.zero;
    void Translate(Vector3 toPoint, float speed)
    {
        float dist = Vector3.Distance(transform.position, toPoint);
        float currentSpeed = speed *dist;
        currentSpeed = Mathf.Clamp(currentSpeed, 0.3f, speed);
        //Debug.Log(currentSpeed);

        transform.position = Vector3.MoveTowards(transform.position, toPoint, Time.deltaTime * currentSpeed);
        
        //--------almost used this but didnt quite work--------------
        //transform.position = Vector3.SmoothDamp(transform.position, toPoint, ref vel, tweenValue);
        if(!audioIsPlaying){
            audioIsPlaying = true;
            doorMoveAudio.Play();
        }
    }

    public void SetOpen(bool open)
    {
        shouldBeOpen = open; 
    }

}
