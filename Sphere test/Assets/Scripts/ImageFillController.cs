using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFillController : MonoBehaviour
{
    public Image imageToControl;
    public int maxValue;
    public int currentValue;
    public GetVelocity getVelocity;


    private void Start()
    {
        getVelocity = GetComponent<GetVelocity>();
    }
    private void Update()
    {
        currentValue = getVelocity.speedForUI;

        float fillAmount = (float)currentValue / maxValue;
        imageToControl.fillAmount = fillAmount;
    }
}

