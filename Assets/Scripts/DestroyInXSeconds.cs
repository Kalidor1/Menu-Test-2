using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInXSeconds : MonoBehaviour
{
    public float seconds = 5f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(Time.time > seconds)
        {
            Destroy(gameObject);
        }
    }
}
