using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodInstanceDestroyer : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Destroy GameObject after 3 frames
        Destroy(gameObject, 3f);        
    }
}
