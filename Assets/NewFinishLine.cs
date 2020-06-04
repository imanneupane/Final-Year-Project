using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFinishLine : MonoBehaviour
{
    public GameObject wall;
    public GameObject Extend1;
    public GameObject Extend2;
    public GameObject Extend3;
    public GameObject ExistingT;
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.name == "FinishLine")
        {

            wall.SetActive(false);
            Extend1.SetActive(true);
            Extend2.SetActive(true);
            Extend3.SetActive(true);
            ExistingT.SetActive(false);
        }
    }
}
