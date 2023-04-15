using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingAxe : MonoBehaviour
{
    [Header("numbers")]
    public float speed = 1.05f;
    public float limitx = 75f;
    public float Yrotation = 90f;
    public bool randomStart = false;
    private float random = 0;
   

    void Awake()
    {
        if (randomStart)
        {
            random = Random.Range(0f, 1f);
        }
       
    }

    void Update()
    {
        float anglex = limitx * Mathf.Sin(Time.time + random * speed); 
        //Time.time is used so then it begins to swing at the beginning of the game         
        
        transform.localRotation = Quaternion.Euler(anglex,Yrotation,0);
    }
}
