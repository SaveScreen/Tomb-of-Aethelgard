using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RupeeScript : MonoBehaviour
{
    
    public int rupeeValue = 1;
    public float speed = 1.05f;
    public float rotationSpeed = 30f;
    public float hoverHeight = 0.5f;
    public bool randomStart = true;
    public float magnetSpeed = 20f;
    private float random = 0;
    private float totalOffset = 0;
    private float totalScale = 1;
    private bool collected = false;

    void Start(){
        totalScale = transform.localScale.x;

        if (randomStart)
        {
            random = Random.Range(0f, 1f);
        }
    }

    void Update()
    {
        if(collected){
            //spin quickly
            rotationSpeed += 20f;

            //fly away
            totalOffset += 50.0f*Time.deltaTime;

            //shrink
            totalScale = Mathf.Clamp(totalScale - (2f * Time.deltaTime), 0.01f, 100f);
            Vector3 newScale = new Vector3(totalScale, totalScale, totalScale);
            transform.localScale = newScale;
        }

        float heightOffset = totalOffset + ((Mathf.Sin(random + (Time.time * speed)))*hoverHeight); 

        Vector3 offsetVector = new Vector3(0, heightOffset * Time.deltaTime, 0);
        Vector3 rotationOffset = new Vector3(0, rotationSpeed*Time.deltaTime, 0);

        transform.position = transform.position + offsetVector;
        //Debug.Log(transform.position);
        transform.Rotate(rotationOffset);
        
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "RupeeMagnetZone"){
            MoveTowardsPlayer(other.gameObject);
        }
        
        if(other.gameObject.tag == "Player"){
            if(other.gameObject.GetComponent<PlayerScript>() == null){
                return;
            }else{
                if(!collected){
                    other.gameObject.GetComponent<PlayerScript>().AddRupees(rupeeValue);
                    PlayCollectedAnimation();
                }
            }
        }
    }

    private void OnTriggerStay(Collider other){
        if(other.gameObject.tag == "RupeeMagnetZone"){
            if(!collected){
                MoveTowardsPlayer(other.gameObject);
            }
            
        }
    }

    private void PlayCollectedAnimation(){
        collected = true;
        totalOffset += 0.1f;
        GetComponent<AudioSource>().Play();
    }

    private void MoveTowardsPlayer(GameObject target){
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, target.transform.position, magnetSpeed*Time.deltaTime);
        Debug.Log("moving towards player");
    }
}
