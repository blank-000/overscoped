using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSelf : MonoBehaviour
{

    public float timeToDeath;
    public float deathTimer{get; private set;}
    
    void Awake(){
        deathTimer = timeToDeath;
    }

    void Update()
    {
        deathTimer -= Time.deltaTime;



        if(deathTimer < 0)
        {
            this.gameObject.SetActive(false);
            Seppuku();
        }
    }



    void Seppuku()
    {
        Destroy(this.gameObject);
    }
}
