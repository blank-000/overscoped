using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAnimHelper : MonoBehaviour
{
    Golem mainGolem;

    void OnEnable()
    {
        mainGolem = GetComponentInParent<Golem>();
    }

    public void ResetSmash()
    {
        mainGolem.ResetSmash();
    }

    public void DestroyChicken()
    {
        Destroy(mainGolem.SmashTarget.gameObject);
        GameObject iceBlock = Instantiate(mainGolem.iceBlockPrefab);
        iceBlock.transform.position = mainGolem.SmashHitPoint.position;
        iceBlock.transform.rotation = Quaternion.Euler(0, Random.Range(0,360), 0);
    }
}
