using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropController : MonoBehaviour
{
    public float initialHealth = 1000;
    public float health = 1000;
    private GUIStyle guiStyleFore;
    private GUIStyle guiStyleBack;

    void Start()
    {
        guiStyleFore = new GUIStyle();
        guiStyleFore.normal.textColor = Color.black;
        guiStyleFore.alignment = TextAnchor.UpperCenter;
        guiStyleFore.fontSize = 20;

        guiStyleBack = new GUIStyle();
        guiStyleBack.normal.textColor = Color.white;
        guiStyleBack.normal.background = Texture2D.whiteTexture;
        guiStyleBack.alignment = TextAnchor.UpperCenter;
        guiStyleBack.fontSize = 20;
        guiStyleBack.fixedHeight = 25;
    }

    void Update()
    {
        if (health <= 0)
        {
            SceneController.Instance.LoadScene("GameOver");
        }
    }

    void OnGUI()
    {
        var text = health.ToString() + "/" + initialHealth.ToString();
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Label(new Rect(pos.x - 50, Screen.height - pos.y - 50, 100, 100), text, guiStyleBack);
        GUI.Label(new Rect(pos.x - 50, Screen.height - pos.y - 50, 100, 100), text, guiStyleFore);
    }
}
