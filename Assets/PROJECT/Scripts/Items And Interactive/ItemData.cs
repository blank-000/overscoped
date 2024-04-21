using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    public Mesh mesh;
    public Material mat;   

    public List<Tag> tags;

    public ItemData proccesedForm;
}
