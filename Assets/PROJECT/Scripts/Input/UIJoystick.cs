using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIJoystick : MonoBehaviour
{

    float maxDistance;
    Transform parent;

    bool IsPressed;

    public void OnRelease(InputAction.CallbackContext ctx)
    {
        IsPressed = false;
    }
    public void UIJoystickPress()
    {
        IsPressed = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsPressed) return;

        
    }
}
