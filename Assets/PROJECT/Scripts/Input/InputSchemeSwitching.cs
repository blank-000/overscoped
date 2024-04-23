using UnityEngine;
using UnityEngine.InputSystem;

public class InputSchemeSwitching : MonoBehaviour
{
    public bool RunInInspector;
    public GameObject OnScreenControls;

    public void OnSwitch(PlayerInput input)
    {
        if(RunInInspector) return;
        if(input.currentControlScheme == "Keyboard&Mouse" || input.currentControlScheme == "Gamepad")
        {
            
            OnScreenControls.SetActive(false);
        }
        
        else
        {
            OnScreenControls.SetActive(true);
            
        }
    }
}
