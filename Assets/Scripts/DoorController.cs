using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    //position to teleport the player
    public GameObject targetPosition;
    public bool playerIsNear = false;

    public string description = "Press E to enter";

    private GUIStyle guiStyleFore;
    private GUIStyle guiStyleBack;
    private bool visible = false;

    void Start()
    {
        guiStyleFore = new GUIStyle();
        guiStyleFore.normal.textColor = Color.white;
        guiStyleFore.alignment = TextAnchor.UpperCenter;
        guiStyleFore.fontSize = 20;

        guiStyleBack = new GUIStyle();
        guiStyleBack.normal.textColor = Color.black;
        guiStyleBack.alignment = TextAnchor.UpperCenter;
        guiStyleBack.fontSize = 21;

    }

    void Update()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.E) && GameController.Instance.canEnter)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = targetPosition.transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
            visible = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            visible = false;
        }
    }

    private void OnGUI()
    {
        if (visible && GameController.Instance.canEnter)
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
            GUI.Label(new Rect(pos.x - 50, Screen.height - pos.y - 50, 100, 100), description, guiStyleBack);
            GUI.Label(new Rect(pos.x - 50, Screen.height - pos.y - 50, 100, 100), description, guiStyleFore);
        }
    }
}
