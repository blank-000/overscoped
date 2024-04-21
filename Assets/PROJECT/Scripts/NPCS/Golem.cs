using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    [SerializeField] float ScanRange = 2f;
    [SerializeField] float DetectionRadius;
    [SerializeField] float AttackRange = 3f;
    [SerializeField] LayerMask Targets; 
    public Transform SmashHitPoint;
    public GameObject iceBlockPrefab;

    [HideInInspector] public Transform SmashTarget;

    Animator animator;
    PlaySounds sounds;
    bool isSmashing;
    

    void Start()
    {
        sounds = GetComponent<PlaySounds>();
        animator = GetComponentInChildren<Animator>();
    }


    void ScanForTarget(){

        RaycastHit hit;
        if (Physics.SphereCast(transform.position + Vector3.up, DetectionRadius, transform.forward, out hit, ScanRange, Targets))
        {
            SmashTarget = hit.transform;
        }
    }



    void Update()
    {
        
        
        ScanForTarget();
        if (SmashTarget == null) return;
    
        transform.LookAt(SmashTarget);
        
        if(Vector3.Distance(SmashTarget.position, transform.position) < AttackRange)
        {   
            if(!isSmashing)
            {
                Smash();
            }
        }
        
    }


    void Smash()
    {
        sounds.PlaySpecial();
        isSmashing = true;
        animator.SetTrigger("Smash");

    }

    public void ResetSmash()
    {
        isSmashing = false;
    }


}
