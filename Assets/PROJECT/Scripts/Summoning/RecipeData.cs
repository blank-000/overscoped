using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe")]
public class RecipeData : ScriptableObject
{
    public ItemData[] ingredients;

    public GameObject result;
}
