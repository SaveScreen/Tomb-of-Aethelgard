using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubDoorScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (CompletionManagerScript.allclear == true) {
            Destroy(gameObject);
        }
    }
}
