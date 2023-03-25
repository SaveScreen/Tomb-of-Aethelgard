using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reflectionManager : MonoBehaviour
{
    public GameObject[] signalCatchers;
    public GameObject[] doors;

    private void Update()
    {
        DoorControlType("isActivated", 2, 1);
        /*This means that when the IsActivated value of catcher 2 is true, door 1 opens. 
        Otherwise, it closes.*/

        DoorControlType("hasBeenActivated", 1, 2, 2);
        /*This means that when catchers 1 and 2 have been activated at some point, door 2 opens.
        It does not matter which order. It does not matter if they are currently being activated.*/
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
    
}
