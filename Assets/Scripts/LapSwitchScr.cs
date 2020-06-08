using UnityEngine;

public class LapSwitchScr : MonoBehaviour
{
    public GameObject finishLine;
    public AIpathChange[] changePath;
    public AIscript[] currentPath;
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.name == "LapSwitch")
        {
            finishLine.SetActive(true);
            foreach (AIscript scr in currentPath)
            {
                scr.enabled = false;
            }
            foreach (AIpathChange cScr in changePath)
            {
                cScr.enabled = true;
            }

        }
    }

}