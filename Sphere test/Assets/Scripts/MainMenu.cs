using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private GameObject playerRock;
    Animator rockAnimator;
    private GameObject uiCanvas;
    Animator canvasAnimator;
    bool fadeIsOn;
    int alpha = 255;
    [SerializeField] Image[] img = new Image[4];
    [SerializeField] TMP_Text[] txt = new TMP_Text[2];
    // Start is called before the first frame update
    void Start()
    {
        playerRock = GameObject.Find("RocaPersonaje");
        rockAnimator = playerRock.GetComponent<Animator>();
        uiCanvas = GameObject.Find("Button Panel");
        canvasAnimator = uiCanvas.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(alpha);
        if (Input.anyKeyDown)
            fadeIsOn = true;

        for (int i = 0; i < 4; i++)
            img[i].color = new Color32(255, 255, 255, (byte)alpha);
        for (int i = 0; i < 2; i++)
            txt[i].color = new Color32(255, 255, 255, (byte)alpha);

        if (alpha < 1)
            rockAnimator.SetBool("fadeComplete", true);
        if (alpha < 1)
            canvasAnimator.SetBool("buttonAppears", true);


    }

    IEnumerator ButtonAppears()
    {
        yield return new WaitForSeconds(2);
        canvasAnimator.SetBool("buttonAppears", true);
    }

    private void FixedUpdate()
    {
        if (fadeIsOn && alpha > 1)
            alpha -= 5;
    }
}
