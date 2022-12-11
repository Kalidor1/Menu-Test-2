using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        var scene = SceneManager.GetActiveScene();
        if (scene.name == "FinishedScene")
        {
            // Set gameobject active
            Debug.Log("FinishedScene");
            gameObject.SetActive(true);
        }
    }
}
