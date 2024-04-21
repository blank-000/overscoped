using System;
using UnityEngine;

public class Item : MonoBehaviour
{

    public ItemData Data;

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    Transform targetPosition;


    void Awake()
    {

        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        targetPosition = transform;

        UpdateItemData(Data);
    }

    void Update()
    {
        transform.position = targetPosition.position;
    }

    public void PlaceItem(Transform target)
    {
        
        transform.parent = target;
        targetPosition = target;
       
    }

    public void UpdateItemData(ItemData _data)
    {
        Data = _data;
        meshFilter.mesh = Data.mesh;
        meshRenderer.material = Data.mat;

    }


}
