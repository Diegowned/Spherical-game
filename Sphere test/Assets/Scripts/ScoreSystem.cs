using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text multiplierText;
    public float baseMultiplierDuration = 10f;
    public int pickupsForMultiplierIncrease = 3;
    private int score = 0;
    private int multiplier = 1;
    private float multiplierTimer = 0f;
    private int pickupsCollectedDuringMultiplier = 0;

    private void Update()
    {
        if (multiplierTimer > 0)
        {
            multiplierTimer -= Time.deltaTime;
            multiplierText.text = "Multiplier: " + multiplier + "x (" + multiplierTimer.ToString("0") + "s)";
        }
        else
        {
            multiplier = 1;
            multiplierText.text = "Multiplier: " + multiplier + "x";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            score += 1 * multiplier;
            scoreText.text = "Score: " + score;

            if (multiplierTimer > 0)
            {
                pickupsCollectedDuringMultiplier++;
                if (pickupsCollectedDuringMultiplier >= pickupsForMultiplierIncrease)
                {
                    multiplier++;
                    pickupsCollectedDuringMultiplier = 0;
                }
            }
            else
            {
                multiplier = 2;
            }

            multiplierTimer = baseMultiplierDuration;
            multiplierText.text = "Multiplier: " + multiplier + "x (" + multiplierTimer.ToString("0") + "s)";
            Destroy(other.gameObject);
        }
    }
}


