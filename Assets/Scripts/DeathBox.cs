using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    [Header("DeathBox")]
    private PlayerScript PS;

    void Awake()
    {
        GameObject PlayerObject = GameObject.FindWithTag("Player");
        if (PlayerObject != null)
        {
            PS = PlayerObject.GetComponent<PlayerScript>();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (PS != null)
        {
            Debug.Log("cock");
        }
    }
}
