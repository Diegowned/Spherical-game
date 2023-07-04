using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class TutorialGuy : MonoBehaviour
{
    public AnimationCurve speed;
    public NPCConversation Tutorial;
    public GameObject soHead;
    public GameObject player;
    public GameObject crosshair;
    public Timer timerScript;


    private void Start()
    {
        timerScript = GameObject.Find("GameManager").GetComponent<Timer>();
    }
    void Update()

    {
        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                ConversationManager.Instance.PressSelectedOption();
        }
        if (ConversationManager.Instance.IsConversationActive == false)
        {
            timerScript.isPaused = false;
            soHead.SetActive(false);
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            crosshair.SetActive(true);
        }
        transform.position = new Vector3(transform.position.x, speed.Evaluate((Time.time % speed.length)), transform.position.z);
    }
    public void OnTriggerEnter(Collider other)
    {
        timerScript.isPaused = true;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        crosshair.SetActive(false);
        soHead.SetActive(true);
        ConversationManager.Instance.StartConversation(Tutorial);
    }
}