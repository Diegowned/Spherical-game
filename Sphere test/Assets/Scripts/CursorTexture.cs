using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTexture : MonoBehaviour
{
    public Texture2D cursorTexture; // The texture to use as cursor
    public Vector2 cursorHotspot = Vector2.zero; // The cursor hotspot (usually center of texture)

    void Start()
    {
        // Hide the default cursor
        Cursor.visible = false;
    }

    void OnGUI()
    {
        // Draw the cursor texture at the cursor position
        GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, cursorTexture.width, cursorTexture.height), cursorTexture);
    }

    void OnDestroy()
    {
        // Show the default cursor when this script is destroyed
        Cursor.visible = true;
    }
}

