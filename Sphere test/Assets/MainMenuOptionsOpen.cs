using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuOptionsOpen : MonoBehaviour
{
    public GameObject optionMenu;

    public void doOpenOptionsMenu()
    {
        optionMenu.SetActive(true);
    }

    public void doCloseOptionsMenu()
    {
        optionMenu.SetActive(false);
    }
}
