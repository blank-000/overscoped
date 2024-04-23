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
                int randomIndex = Random.Range(0, matchingPrefabs.Count);
                if(matchingPrefabs[randomIndex] != lastSpawned)
                {
                    //for some reason this works ok most of the time but has a problem with the tornado and white sludge(white, sus), it spawns many tornados(sus) instead of switching
                    lastSpawned = matchingPrefabs[randomIndex];
                    return matchingPrefabs[randomIndex];
                } else 
                {
                    if(randomIndex > 0)
                    {
                        int secondRandomIndex = Random.Range(0, randomIndex);
                        lastSpawned = matchingPrefabs[secondRandomIndex];
                        return matchingPrefabs[secondRandomIndex];
                    } else if (matchingPrefabs.Count > 1)
                    {
                        lastSpawned = matchingPrefabs[randomIndex+1];
                        return matchingPrefabs[randomIndex+1];
                    }else{
                        lastSpawned = matchingPrefabs[randomIndex];
                        return matchingPrefabs[randomIndex];
                    }

                }
                 
                
            }
            else
            {
                return null; // Summoning failed
            }
        } else 
        {
            
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
                    Debug.Log(prefab);
                }
                
            }

            if (matchingPrefabs.Count > 0)
            {   
                int randomIndex = Random.Range(0, matchingPrefabs.Count);
                if(matchingPrefabs[randomIndex] != lastSpawned)
                {
                    //for some reason this works ok most of the time but has a problem with the tornado and white sludge(white, sus), it spawns many tornados(sus) instead of switching
                    lastSpawned = matchingPrefabs[randomIndex];
                    return matchingPrefabs[randomIndex];
                } else 
                {
                    if(randomIndex > 0)
                    {
                        int secondRandomIndex = Random.Range(0, randomIndex);
                        lastSpawned = matchingPrefabs[secondRandomIndex];
                        return matchingPrefabs[secondRandomIndex];
                    } else if (matchingPrefabs.Count > 1)
                    {
                        lastSpawned = matchingPrefabs[randomIndex+1];
                        return matchingPrefabs[randomIndex+1];
                    }else{
                        lastSpawned = matchingPrefabs[randomIndex];
                        return matchingPrefabs[randomIndex];
                    }

                }
                 
                
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