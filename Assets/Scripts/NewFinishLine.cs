using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFinishLine : MonoBehaviour
{
    public GameObject trackExtension;
    public GameObject wallAppear;
    public GameObject wallDisappear;
    public GameObject trackExtra;
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.name == "LapSwitch")
        {
            trackExtension.SetActive(true);
            wallAppear.SetActive(true);
            wallDisappear.SetActive(false);
            trackExtra.SetActive(false);
        }
    }
}
