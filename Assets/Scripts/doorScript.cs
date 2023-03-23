using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    public GameObject closedPosition;
    public GameObject openPosition;

    public bool startsOpen;
    private bool shouldBeOpen;
    public float openSpeed = 2;
    public float closeSpeed = 1;

    public AudioSource doorMoveAudio;
    private bool audioIsPlaying = false;

    public int doorID;

    void Start()
    {
        shouldBeOpen = startsOpen;
    }

    void Update()
    {
        /*
        if (shouldBeOpen && transform.position != openPosition.transform.position)
        {
            //transform.position = openPosition.transform.position;
            Translate(openPosition.transform.position, openSpeed);
            Debug.Log("1");
        } else if (!shouldBeOpen && transform.position != closedPosition.transform.position)
        {
            //transform.position = closedPosition.transform.position;
            Translate(closedPosition.transform.position, closeSpeed);
            Debug.Log("2");
        }*/

        if (shouldBeOpen == true)
        {
            if (transform.position != openPosition.transform.position)
            {
                Translate(openPosition.transform.position, openSpeed);
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
            }
            else
            {
                //rest
                audioIsPlaying = false;
                doorMoveAudio.Stop();
            }
        }
    }
    

    void Translate(Vector3 toPoint, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, toPoint, Time.deltaTime * speed);
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
