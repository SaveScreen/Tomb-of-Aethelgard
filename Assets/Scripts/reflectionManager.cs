using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reflectionManager : MonoBehaviour
{
    public GameObject[] signalCatchers;
    public GameObject[] doors;
    public GameObject[] filters;

    public GameObject[] projectors;

    private bool[,] raysHitReceivers = new bool[50, 50];
    private bool[,] raysHitFilters = new bool[50, 50];

    private void Start(){
        for(int i = 0; i < projectors.Length;i++){
            for(int j = 0; j < filters.Length;j++){
                raysHitFilters[i, j]= false;
            }
        }
        for(int i = 0; i < projectors.Length;i++){
            for(int j = 0; j < signalCatchers.Length;j++){
                raysHitReceivers[i, j]= false;
            }
        }
    }

    private void LateUpdate()
    {
        if(SceneManager.GetActiveScene().name == "TutorialLevel"){
            DoorControlType("isActivated", 1, 1);
            DoorControlType("isActivated", 2, 2);
        }
        if(SceneManager.GetActiveScene().name == "VSLevelScene"){
            DoorControlType("isActivated", 1, 1);
            DoorControlType("isActivated", 2, 2);
        }
        if(SceneManager.GetActiveScene().name == "Level2Scene"){
            DoorControlType("isActivated", 1, 1);
            DoorControlType("isActivated", 2, 3, 2);
        }
        if(SceneManager.GetActiveScene().name == "Level3Scene"){
            DoorControlType("isActivated", 1, 1);
            DoorControlType("isActivated", 2, 2);
            DoorControlType("isActivated", 3, 3);
        }
        if (SceneManager.GetActiveScene().name == "PrismTestScene")
        {
            DoorControlType("isActivated", 1, 1);
            DoorControlType("isActivated", 2, 2);
            DoorControlType("isActivated", 3, 3);
        }

        // DoorControlType("hasBeenActivated", 1, 2, 2);
        /*This means that when catchers 1 and 2 have been activated at some point, door 2 opens.
        It does not matter which order. It does not matter if they are currently being activated.*/
        SetProjectors(); 
        CheckReceivers();
        CheckFilters();

        //Debug.Log("Receivers: " + raysHitReceivers);
        //Debug.Log("Filters: " + raysHitFilters);
        //Debug.Log("Projector 1 on Filters: " + raysHitFilters[0, 0] + raysHitFilters[0, 1] + raysHitFilters[0, 2]);
        //Debug.Log("Projector 2 on Filters: " + raysHitFilters[1, 0] + raysHitFilters[1, 1] + raysHitFilters[1, 2]);
        //Debug.Log("Projector 3 on Filters: " + raysHitFilters[2, 0] + raysHitFilters[2, 1] + raysHitFilters[2, 2]);
        Debug.Log("Projector 1 on Receivers: " + raysHitReceivers[0, 0] + raysHitReceivers[0, 1] + raysHitReceivers[0, 2]);
        Debug.Log("Projector 2 on Receivers: " + raysHitReceivers[1, 0] + raysHitReceivers[1, 1] + raysHitReceivers[1, 2]);
        Debug.Log("Projector 3 on Receivers: " + raysHitReceivers[2, 0] + raysHitReceivers[2, 1] + raysHitReceivers[2, 2]);
        Debug.Log("Projector 4 on Receivers: " + raysHitReceivers[3, 0] + raysHitReceivers[3, 1] + raysHitReceivers[3, 2]);
        Debug.Log("Projector 5 on Receivers: " + raysHitReceivers[4, 0] + raysHitReceivers[4, 1] + raysHitReceivers[4, 2]);
        Debug.Log("Projector 6 on Receivers: " + raysHitReceivers[5, 0] + raysHitReceivers[5, 1] + raysHitReceivers[5, 2]);
        Debug.Log("Projector 7 on Receivers: " + raysHitReceivers[6, 0] + raysHitReceivers[6, 1] + raysHitReceivers[6, 2]);
        Debug.Log("Projector 8 on Receivers: " + raysHitReceivers[7, 0] + raysHitReceivers[7, 1] + raysHitReceivers[7, 2]);
        Debug.Log("Projector 9 on Receivers: " + raysHitReceivers[8, 0] + raysHitReceivers[8, 1] + raysHitReceivers[8, 2]);
    }

    private void DoorControl(int rID, int doorID)
    {
        if (signalCatchers[rID - 1].GetComponent<signalCatcherScript>().GetHasBeenActivated())
        {
            doors[doorID - 1].GetComponent<doorScript>().SetOpen(true);
        }
    }

    private void DoorControlType(string type, int rID, int doorID)
    {
        if(type == "hasBeenActivated"){
            if (signalCatchers[rID - 1].GetComponent<signalCatcherScript>().GetHasBeenActivated())
            {
                doors[doorID - 1].GetComponent<doorScript>().SetOpen(true);
            }    
        }
        if(type == "isActivated"){
            if (signalCatchers[rID - 1].GetComponent<signalCatcherScript>().GetIsActivated())
            {
                //Debug.Log("door " + (doorID - 1) + " has been set to true");
                doors[doorID - 1].GetComponent<doorScript>().SetOpen(true);
            }else{
                
                doors[doorID - 1].GetComponent<doorScript>().SetOpen(false);
            }
        }
        
    }

    private void DoorControlType(string type, int rID1, int rID2, int doorID)
    {
        if(type == "hasBeenActivated"){
            if (signalCatchers[rID1 - 1].GetComponent<signalCatcherScript>().GetHasBeenActivated() && signalCatchers[rID2 - 1].GetComponent<signalCatcherScript>().GetHasBeenActivated())
            {
                doors[doorID - 1].GetComponent<doorScript>().SetOpen(true);
            }    
        }
        if(type == "isActivated"){
            if (signalCatchers[rID1 - 1].GetComponent<signalCatcherScript>().GetIsActivated() && signalCatchers[rID2 - 1].GetComponent<signalCatcherScript>().GetHasBeenActivated())
            {
                //Debug.Log("door " + (doorID - 1) + " has been set to true");
                doors[doorID - 1].GetComponent<doorScript>().SetOpen(true);
            }else{
                
                doors[doorID - 1].GetComponent<doorScript>().SetOpen(false);
            }
        }
        
    }

    private void DoorControl(int rID1, int rID2, int doorID)
    {
        if (signalCatchers[rID1-1].GetComponent<signalCatcherScript>().GetHasBeenActivated() && signalCatchers[rID2 - 1].GetComponent<signalCatcherScript>().GetHasBeenActivated())
        {
            doors[doorID-1].GetComponent<doorScript>().SetOpen(true);
        }
    }

    private void DoorControl(int rID1, int rID2, int rID3, int doorID)
    {
        if (signalCatchers[rID1 - 1].GetComponent<signalCatcherScript>().GetHasBeenActivated() && signalCatchers[rID2 - 1].GetComponent<signalCatcherScript>().GetHasBeenActivated() && signalCatchers[rID3 - 1].GetComponent<signalCatcherScript>().GetHasBeenActivated())
        {
            doors[doorID - 1].GetComponent<doorScript>().SetOpen(true);
        }
    }


    /*These 2 methods below are used to make signalCatchers "isActivated" value become false
    when they are not being hit*/
    public void SetAllCatchersIsActivated(bool on){
        for(int i = 0; i < signalCatchers.Length; i++){
            signalCatchers[i].GetComponent<signalCatcherScript>().SetIsActivated(on);
        }
    }

    public void SetAllCatchersIsActivatedExcept(bool on, int hitID){
        for(int i = 0; i < signalCatchers.Length; i++){
            if(i != hitID-1){
                signalCatchers[i].GetComponent<signalCatcherScript>().SetIsActivated(on);
            }
        }
    }

    public void TurnOffFilters(){
        for(int i = 0; i < filters.Length; i++){
            filters[i].GetComponent<raycastScript>().SetIsProjector(false);
        }
    }

    private void SetProjectors(){
        for(int i = 0; i < projectors.Length;i++){

            if(projectors[i].GetComponent<raycastScript>().GetHitSpecialObject() == null){
                for(int j = 0; j < signalCatchers.Length; j++){
                    raysHitReceivers[i, j]= false;
                }
                for(int k = 0; k < filters.Length; k++){
                    raysHitFilters[i, k]= false;
                }
            }
            else{
                //get hit object and record it
                GameObject hitTarget = projectors[i].GetComponent<raycastScript>().GetHitSpecialObject();

                //check if it is a filter
                if(hitTarget.GetComponent<raycastScript>() != null){
                    //activate filter

                    raysHitFilters[i, hitTarget.GetComponent<raycastScript>().filterID-1] = true;
                }
                //check if it is a signal catcher
                if (hitTarget.GetComponent<signalCatcherScript>() != null){
                    //activate signal catcher
                    
                    raysHitReceivers[i, hitTarget.GetComponent<signalCatcherScript>().signalCatcherID-1] = true;
                    hitTarget.GetComponent<signalCatcherScript>().SetHasBeenActivated(true);
                }
                //check if it is a prism
                if (hitTarget.GetComponent<PrismScript>() != null)
                {
                    //activate prism

                    raysHitFilters[i, hitTarget.GetComponent<PrismScript>().filterIDs[0] - 4] = true;
                    raysHitFilters[i, hitTarget.GetComponent<PrismScript>().filterIDs[1] - 4] = true;
                    raysHitFilters[i, hitTarget.GetComponent<PrismScript>().filterIDs[2] - 4] = true;
                }
            }
        }
    }

    private bool CheckOneReceiver(int j){
        for(int i = 0; i < projectors.Length;i++){
            if(raysHitReceivers[i, j]){
                return true;
            }
        }
        return false;
    }

    private bool CheckOneFilter(int j){
        for(int i = 0; i < projectors.Length;i++){
            if(raysHitFilters[i, j]){
                return true;
            }
        }
        return false;
    }

    private void CheckReceivers(){
        for(int j = 0; j < signalCatchers.Length;j++){
            if(CheckOneReceiver(j)){
                signalCatchers[j].GetComponent<signalCatcherScript>().SetIsActivated(true);
            } else{
                signalCatchers[j].GetComponent<signalCatcherScript>().SetIsActivated(false);
            }
        }
    }
    private void CheckFilters(){
        for(int j = 0; j < filters.Length;j++){
            if(CheckOneFilter(j)){
                filters[j].GetComponent<raycastScript>().ActivateFilter(true);
            } else{
                filters[j].GetComponent<raycastScript>().ActivateFilter(false);
            }
            //Debug.Log("Filter " + j + " is " + filters[j].GetComponent<raycastScript>().GetIsProjector());
        }
    }
    
}
