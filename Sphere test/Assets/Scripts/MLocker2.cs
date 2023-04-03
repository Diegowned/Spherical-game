using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLocker2 : MonoBehaviour
{
    public Texture2D cursorTexture;

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        SetCursorTexture();
    }

    private void SetCursorTexture()
    {
        
    }
}
