using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInXSeconds : MonoBehaviour
{
    public float seconds = 5f;
    private bool isGameLoaded = false;
    // Start is called before the first frame update

    // Update is called once per frame

    private void Awake()
    {
        isGameLoaded = true;
        StartCoroutine(Countdown());
      
    }


    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
