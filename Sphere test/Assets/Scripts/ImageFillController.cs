using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFillController : MonoBehaviour
{
    public float speedlineValue = 5f; // the value at which the particle effect should be activated
    public ParticleSystem particleEffectPrefab; // the particle effect prefab to be activated
    public Image imageToControl;
    public int maxValue;
    public int currentValue;
    public GetVelocity getVelocity;


    private void Start()

    {
        particleEffectPrefab = GameObject.Find("Particle System").GetComponent<ParticleSystem>(); ;
        getVelocity = GetComponent<GetVelocity>();
    }
    private void Update()
    {
        if (currentValue > speedlineValue)
        {
            particleEffectPrefab.Play();

        }

        currentValue = getVelocity.speedForUI;

        float fillAmount = (float)currentValue / maxValue;
        imageToControl.fillAmount = fillAmount;
    }
}

