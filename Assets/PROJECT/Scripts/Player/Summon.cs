using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Summon : MonoBehaviour
{
    SummoningCircle circle;
    PlayerAnim _anim;
    // Start is called before the first frame update
    void Start()
    {
        circle = FindFirstObjectByType<SummoningCircle>();
        _anim = GetComponentInChildren<PlayerAnim>();
    }

    public void OnSummon(InputAction.CallbackContext ctx)
    {
        if(ctx.started){
            circle.Activate();
            _anim.PlaySummonClip();
        }
        
    }
}
