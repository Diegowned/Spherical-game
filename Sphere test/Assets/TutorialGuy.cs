using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class TutorialGuy : MonoBehaviour
{
    public AnimationCurve speed;
    public NPCConversation Tutorial;
    public GameObject soHead;
    void Update()

    {
        transform.position = new Vector3(transform.position.x, speed.Evaluate((Time.time % speed.length)), transform.position.z);
    }
    public void OnTriggerEnter(Collider other)
    {
        soHead.SetActive(true);
        ConversationManager.Instance.StartConversation(Tutorial);
        
    }
}