using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLocker2 : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public bool cursorVisible;


    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = cursorVisible;
        SetCursorTexture();
    }

    private void SetCursorTexture()
    {
        Cursor.SetCursor(cursorTexture,hotSpot,cursorMode);
    }
}
