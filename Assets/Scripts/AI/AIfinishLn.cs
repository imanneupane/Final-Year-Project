using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIfinishLn : MonoBehaviour
{
    public AIscript bot;
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.name == "FinishLine")
        {
            bot.enabled = true;
        }
    }
}
