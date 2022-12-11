using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{

    public int waitingTime;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Wait());
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitingTime);
        Debug.Log("Waited");
      

    }
}
