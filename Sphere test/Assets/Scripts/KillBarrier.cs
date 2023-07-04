using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBarrier : MonoBehaviour
{
    public ParticleSystem poofParticle;
    public float destructionDelay;
    public float sceneLoadDelay;
    public GameObject player;
    public AudioSource poofSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player/Sphere/RocaPersonaje");
        poofParticle = GameObject.Find("Player/CFXR Magic Poof").GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ExplodeAndDestroyPlayer(other.gameObject));
        }
    }

    private IEnumerator ExplodeAndDestroyPlayer(GameObject player)
    {
        Debug.Log("Starting Explosion");
        yield return new WaitForSecondsRealtime(destructionDelay);


        // Disable the player's collider and renderer
        player.gameObject.SetActive(false);
        // Play explosion particle effect
        poofSoundEffect.Play();
        poofParticle.Play();

        yield return new WaitForSeconds(sceneLoadDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }
}
