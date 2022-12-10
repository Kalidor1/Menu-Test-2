using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public string name = "Ziege";
    public string description = "Eine Ziege";
    public string property;
    public float value;

    // This is for the tooltip
    private GUIStyle guiStyleFore;
    private GUIStyle guiStyleBack;
    private bool visible = false;

    public Item(Item item)
    {
        name = item.name;
        description = item.description;
        property = item.property;
        value = item.value;
    }

    public void Start()
    {
        guiStyleFore = new GUIStyle();
        guiStyleFore.normal.textColor = Color.black;
        guiStyleFore.alignment = TextAnchor.UpperCenter;
        guiStyleFore.fontSize = 20;

        guiStyleBack = new GUIStyle();
        guiStyleBack.normal.textColor = Color.white;
        guiStyleBack.alignment = TextAnchor.UpperCenter;
        guiStyleBack.fontSize = 20;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        visible = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        visible = false;
    }

    private void OnGUI()
    {
        if (visible)
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
            GUI.Label(new Rect(pos.x - 50, Screen.height - pos.y - 50, 100, 100), name, guiStyleBack);
            GUI.Label(new Rect(pos.x - 50, Screen.height - pos.y - 50, 100, 100), name, guiStyleFore);
        }
    }
}
