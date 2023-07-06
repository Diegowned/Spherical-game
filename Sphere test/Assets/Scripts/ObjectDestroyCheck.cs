using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectDestroyCheck : MonoBehaviour
{
    public GameObject objectToCheck;
    public float delayBeforeSceneLoad = 3f; // Delay in seconds before loading the scene
    public string sceneToLoad;
    public AudioManager audioManager;

    private bool isObjectDestroyed = false;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        objectToCheck = GameObject.Find("Drill Final");
    }

    private void Update()
    {
        // Check if the object has been destroyed
        if (objectToCheck == null && !isObjectDestroyed)
        {
            audioManager.PlaySFX(audioManager.explosionSound);
            isObjectDestroyed = true;
            StartCoroutine(LoadSceneAfterDelay());
        }
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeSceneLoad);
        SceneManager.LoadScene(sceneToLoad); // Replace "YourSceneName" with the actual scene name you want to load
    }
}

