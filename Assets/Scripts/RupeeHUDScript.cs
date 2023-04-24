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
    
    //never used boolean
    //private bool atTarget = true;
    float activeTimer;
    public float timerDuration = 5.0f;
    public float moveSpeed = 20f;
    public float distanceFromTopOfScreen = 50f;
    public float fractionalDistanceFromTopOfScreen = 0.2f;
    private float timeGoal; 

    void Start()
    {
        distanceFromTopOfScreen = fractionalDistanceFromTopOfScreen*Screen.height;

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
            //never used boolean
            //atTarget = true; 
            activeTimer = timerDuration;
        }

        //if(atTarget && !Mathf.Approximately(textElement.transform.position.y, startPosition.y)){
        /*if(atTarget){
            activeTimer -= (Time.deltaTime);
        }
        if(activeTimer <= 0){
            positionTarget = startPosition;
        }*/

        //id like to use Time.deltaTime but it was being buggy and returning absurdly small values
        //no clue why
        //this new solution works below
        
        if(Time.time>= timeGoal){
            positionTarget = startPosition;
        }
    }

    public void UpdateHUD(int amt, int amtChanged){
        //Debug.Log("updating hud");
        rupees = amt + amtChanged;
        rupeeText = "" + rupees;
        textElement.text = rupeeText;
        activeTimer = timerDuration;
        
        //never used boolean
        //atTarget = false;
        positionTarget = startPosition + new Vector3(0f, -1*distanceFromTopOfScreen, 0f);

        timeGoal = Time.time + timerDuration;
        //Debug.Log("timeGoal: " + timeGoal);
        //Debug.Log("time: " + Time.time);
    }
}
