using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RupeeHUDScript : MonoBehaviour
{

    private string rupeeText = "0";
    private int rupees;
    public TextMeshProUGUI textElement;

    Vector3 startPosition;
    Vector3 positionTarget;
    private bool atTarget = true;
    float activeTimer;
    public float timerDuration = 5.0f;
    public float moveSpeed = 20f;
    public float distanceFromTopOfScreen = 50f;

    void Start()
    {
        textElement.text = rupeeText;
        startPosition = textElement.transform.position;
        positionTarget = startPosition;
    }

    void Update(){
        if(!Mathf.Approximately(textElement.transform.position.y, positionTarget.y)){
            //Debug.Log("Current position: " + textElement.transform.position);
            //Debug.Log("target position: " + positionTarget);
            textElement.transform.position = Vector3.MoveTowards(textElement.transform.position, positionTarget, moveSpeed * Time.deltaTime);
        }
        else{
            atTarget = true;
            activeTimer = timerDuration;
        }

        //if(atTarget && !Mathf.Approximately(textElement.transform.position.y, startPosition.y)){
        if(atTarget){
            activeTimer -= (Time.deltaTime);
        }
        if(activeTimer <= 0){
            positionTarget = startPosition;
        }
        //Debug.Log("active timer: " + activeTimer);
    }

    public void UpdateHUD(int amtChanged){
        //Debug.Log("updating hud");
        rupees += amtChanged;
        rupeeText = "" + rupees;
        textElement.text = rupeeText;
        activeTimer = timerDuration;
        atTarget = false;
        positionTarget = startPosition + new Vector3(0f, -1*distanceFromTopOfScreen, 0f);
    }
}
