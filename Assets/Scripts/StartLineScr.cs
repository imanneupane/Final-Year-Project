using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLineScr : MonoBehaviour
{
    public GameObject finishline;

    void OnCollisionEnter(Collision collisionInfo)
    {
        
        if (collisionInfo.collider.name == "StartLine")
        {
            Debug.Log("Start Line crossed!!");
            StartCoroutine(Wait());
        }   
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(10);
        finishline.SetActive(true);
    }
}
