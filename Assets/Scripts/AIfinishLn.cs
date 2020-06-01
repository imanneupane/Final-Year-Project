using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIfinishLn : MonoBehaviour
{
    public AIscript bot;
    public Text finishtxt;
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.name == "FinishLine")
        {
            bot.enabled = false;
            finishtxt.enabled = true;
        }
    }
}
