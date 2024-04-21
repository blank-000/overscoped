using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class DisableSelf : MonoBehaviour
{
    public float timeToDisplay;
    float timer;

    void OnEnable()
    {
        timer = timeToDisplay;
    }
    void Update()
    {
        if(this.gameObject.activeInHierarchy)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
