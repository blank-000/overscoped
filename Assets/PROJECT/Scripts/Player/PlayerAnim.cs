using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator anim;
    PlayerInteraction interact;
    TopDownMovement movement;

    bool isStopped;  

    void Awake()
    {
        anim = GetComponent<Animator>();
        interact = GetComponentInParent<PlayerInteraction>();
        movement = GetComponentInParent<TopDownMovement>();     
    }

    void Update()
    {
        float carryLayerWeight = interact.IsHoldingObject ? 1 : 0;

        anim.SetBool("IsMoving", !movement.IsMoving);
        anim.SetLayerWeight(anim.GetLayerIndex("Add Carry"), carryLayerWeight);
    }

    public void Step(){
        Debug.Log("step");
    }

    public void PlayEnd()
    {
        anim.SetTrigger("End");
    }

    public void PlaySummonClip()
    {
        if(isStopped) return;
        anim.SetLayerWeight(anim.GetLayerIndex("Add Carry"), 0);
        anim.SetTrigger("Summon");
        
        StartCoroutine(WaitForSummon());

    }

    IEnumerator WaitForSummon()
    {
        isStopped =true;
        movement.StopMoving();
  
        
        
        yield return new WaitForSecondsRealtime(1.5f);
        movement.StartMoving();
        isStopped = false;
    }
}
