using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAnimEvents : MonoBehaviour
{
    public Frog mainFrog;



    public void LockRotation()
    {
        Debug.Log("jump");
        mainFrog.canRotate = false;
        
    }

    public void UnlockRotation()
    {
        Debug.Log("Land");
        mainFrog.canRotate = true;
        
    }
}
