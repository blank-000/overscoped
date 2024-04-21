using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public enum Tag{
    // base tags
    alive,
    explosive,
    frozen,
    eye,
    creaturePart,
    sus,
    // resulting tags
    white,
    crystal
}


public class RecipeBook : MonoBehaviour
{
    
    public GameObject[] prefabs;
    GameObject lastSpawned;

    // Evaluate and return the best matching prefab based on item tags
    public GameObject EvaluateRecipe(List<Tag> itemTags, bool canSummonGolem)
    {
        if(canSummonGolem)
        {
            Debug.Log("List contents:");
            foreach (var item in itemTags)
            {
                Debug.Log(item);
            }
            List<GameObject> matchingPrefabs = new List<GameObject>();

            foreach (GameObject prefab in prefabs)
            {
                SummonResult resultComponent = prefab.GetComponent<SummonResult>();
                if (resultComponent != null && TagsMatch(resultComponent.tags, itemTags))
                {
                    //Debug.Log(prefab);
                    matchingPrefabs.Add(prefab);
                }
                
            }

            if (matchingPrefabs.Count > 0)
            {
                return matchingPrefabs[Random.Range(0, matchingPrefabs.Count)];
            }
            else
            {
                return null; // Summoning failed
            }
        } else 
        {
                        Debug.Log("List contents:");
            foreach (var item in itemTags)
            {
                Debug.Log(item);
            }
            List<GameObject> matchingPrefabs = new List<GameObject>();

            foreach (GameObject prefab in prefabs)
            {
                SummonResult resultComponent = prefab.GetComponent<SummonResult>();
                if (resultComponent != null && TagsMatch(resultComponent.tags, itemTags))
                {
                    if (prefab.GetComponent<Golem>() == null)
                    {
                        matchingPrefabs.Add(prefab);
                    } 
                    //Debug.Log(prefab);
                }
                
            }

            if (matchingPrefabs.Count > 0)
            {   
                int randomIndex = Random.Range(0, matchingPrefabs.Count);
                if(matchingPrefabs[randomIndex] != lastSpawned)
                {
                    // implement some pseudo randomness to prevent repeating recipes producing the same result if they can summon multiple things
                }
                 
                return matchingPrefabs[randomIndex];
            }
            else
            {
                return null; // Summoning failed
            }
        }
    }

    // Check if prefab tags match item tags
    private bool TagsMatch(List<Tag> prefabTags, List<Tag> itemTags)
    {
        return prefabTags.All(tag => itemTags.Contains(tag));
    }

    // // Check if all prefab tags are present in the item tags list
    // private bool TagsMatch(List<Tag> prefabTags, List<Tag> itemTags)
    // {
    //     return prefabTags.All(tag => itemTags.Contains(tag));
    // }

}