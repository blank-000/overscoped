using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoShaderController : MonoBehaviour
{
    public float growTime;
    
    KillSelf killer;
    MoveAndBounce move;
    List<Material> materials = new List<Material>();
    float timer;


    void Awake()
    {
        killer = GetComponentInParent<KillSelf>();
        move = GetComponentInParent<MoveAndBounce>();


        timer = growTime;
        foreach(Transform child in transform)
        {   
            if(child.GetComponent<MeshRenderer>() != null)
            {
                materials.Add(child.GetComponent<MeshRenderer>().material);

            }
        }
        foreach(Material mat in materials)
        {
            Debug.Log("mat: " + mat.name);
        }
    }
    void Start()
    {
        foreach(Material mat in materials)
        {
            mat.SetFloat("_DissolveFromTop", 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if(!move.startMoving)
        {
            timer -= Time.deltaTime;
            if(timer > 0)
            {
                float elapsed = timer/growTime;
                float t = 1 - elapsed;

                foreach(Material mat in materials)
                {
                    mat.SetFloat("_DissolveFromTop", elapsed);
                }
            } else {
                move.startMoving = true;
                timer = growTime;
            }
        }
        if (killer.deathTimer < growTime)
        {
            timer -= Time.deltaTime;
            if(timer > 0)
            {
                float elapsed = timer/growTime;
                float t = 1 - elapsed;

                foreach(Material mat in materials)
                {
                    mat.SetFloat("_DissolveFromTop", t);
                }
            }

        }
    }
}
