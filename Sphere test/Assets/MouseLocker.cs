using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocker : MonoBehaviour
{
    public Texture2D cursorTexture; // Texture to display at locked position
    public Vector2 lockedPosition; // Position to lock mouse to
    public KeyCode lockcameraKey;

    private bool isMouseLocked = false; // Is the mouse currently locked?

    private void Update()
    {
        if (Input.GetKeyDown(lockcameraKey))
        {
            if (isMouseLocked)
            {
                UnlockMouse();
            }
            else
            {
                LockMouse();
            }
        }
    }

    private void LockMouse()
    {
        isMouseLocked = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SetCursorTexture();
    }

    private void UnlockMouse()
    {
        isMouseLocked = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void SetCursorTexture()
    {
        Cursor.SetCursor(cursorTexture, lockedPosition, CursorMode.Auto);
    }

    private void OnGUI()
    {
        if (isMouseLocked)
        {
            GUI.DrawTexture(new Rect(lockedPosition.x, Screen.height - lockedPosition.y, cursorTexture.width, cursorTexture.height), cursorTexture);
        }
    }
}

