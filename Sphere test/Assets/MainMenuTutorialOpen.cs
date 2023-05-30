using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTutorialOpen : MonoBehaviour
{

    public GameObject tutorialMenu;


    public void doOpenTutorialMenu()

    {
        tutorialMenu.SetActive(true);
    }

    public void doCloseTutorialMenu()

    {
        tutorialMenu.SetActive(false);
    }

}
