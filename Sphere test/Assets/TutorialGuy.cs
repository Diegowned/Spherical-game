using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuy : MonoBehaviour
{
    public AnimationCurve speed;
    void Update()

    {
        transform.position = new Vector3(transform.position.x, speed.Evaluate((Time.time % speed.length)), transform.position.z);
    }
}