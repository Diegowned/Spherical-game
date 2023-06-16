using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public GameObject prefabToInstantiate; // Reference to the prefab to instantiate
    public TMP_Text scoreText;
    public TMP_Text multiplierText;
    public float baseMultiplierDuration = 10f;
    public int pickupsForMultiplierIncrease = 3;
    private int score = 0;
    private int multiplier = 1;
    private float multiplierTimer = 0f;
    private int pickupsCollectedDuringMultiplier = 0;


    private void Start()
    {
        scoreText = GameObject.Find("Canvas/Score Text").GetComponent<TMP_Text>();
        multiplierText = GameObject.Find("Canvas/Multiplier Text").GetComponent<TMP_Text>();
    }

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

            // Change the scale of the pickup
            Vector3 newScale = new Vector3(1f, 0.01f, 1f); // Set the desired new scale
            other.transform.localScale = newScale;

            // Change the tag of the pickup
            string newTag = "CollectedPickup"; // Set the desired new tag
            other.tag = newTag;

            // Instantiate the prefab on the pickup
            GameObject instantiatedPrefab = Instantiate(prefabToInstantiate, other.transform.position, Quaternion.identity);


        // Adjust the position of the pickup to the ground
        RaycastHit hit;
            if (Physics.Raycast(other.transform.position, Vector3.down, out hit))
            {
                float groundOffset = 0.5f; // Offset to ensure the pickup is above the ground
                Vector3 newPosition = hit.point + hit.normal * groundOffset;
                other.transform.position = newPosition;
            }

            // Start coroutine to delete the pickup and the instantiated prefab after a delay
            StartCoroutine(DeletePickupAndPrefabAfterDelay(other.gameObject, instantiatedPrefab, 2f)); // Change the delay as desired
        }
    }

    private IEnumerator DeletePickupAndPrefabAfterDelay(GameObject pickup, GameObject instantiatedPrefab, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(pickup);
        Destroy(instantiatedPrefab);
    }
}

