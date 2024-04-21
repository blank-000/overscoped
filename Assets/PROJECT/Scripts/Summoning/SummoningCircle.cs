using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningCircle : MonoBehaviour
{   
    PlaySounds sounds;
    public ItemData none;
    public Transform[] slots;
    public GameObject[] dudRecipePrefabs;
    // List<ItemData> items = new List<ItemData>();

    public Transform resultSlot;
    ItemPickUp pickUp;

    [SerializeField] MeshRenderer rend;
    Material mat;

    public RecipeBook recipeBook; // Reference to the recipe book


    [SerializeField] float animationSpeed ;
    bool IsAnimating;
    Vector4 normalEmmisiveCol;
    List<Tag> itemTags;

    public bool canSummonGolem {get; private set;}
    

    float normalVis;

    [SerializeField] float timeToAnimationComplete = 3f;
    float timer;

    void Update()
    {
        if(IsAnimating)
        {
            timer -= Time.deltaTime;
            if(timer > 0)
            {
                float elapsed = timer/timeToAnimationComplete;
                float t = 1 - elapsed;

                
                mat.SetFloat("_Visibility", t);
                mat.SetVector("_EmissionColor", normalEmmisiveCol * 3f * t );

            } else {
                IsAnimating = false;
                timer = timeToAnimationComplete;
                mat.SetFloat("_Visibility", normalVis);
                mat.SetVector("_EmissionColor", normalEmmisiveCol );
                NewMethod(itemTags);
            }

        }
    }


    void Awake()
    {
        timer = timeToAnimationComplete;

        sounds = GetComponent<PlaySounds>();
        mat = rend.material;
        normalEmmisiveCol = mat.GetVector("_EmissionColor");
        normalVis = mat.GetFloat("_Visibility");


        canSummonGolem = true;
        pickUp = resultSlot.GetComponent<ItemPickUp>();
    }


    


    public void Activate()
    {
        if (IsAnimating) return;

        sounds.PlaySpecial();

        itemTags = new List<Tag>();

        foreach (Transform t in slots)
        {
            Item item = t.GetComponentInChildren<Item>();
            if (item != null)
            {
                //items.Add(item.Data);
                itemTags.AddRange(item.Data.tags);
                Destroy(item.gameObject);
            }
            else
            {
                //items.Add(null);
            }
        }


        IsAnimating =true;
    }


  
    private void NewMethod(List<Tag> itemTags)
    {
        // Check recipes using item tags
        List<GameObject> resultPrefabs = new List<GameObject>();

        // I hate AI 
        GameObject resultPrefab = recipeBook.EvaluateRecipe(itemTags, canSummonGolem);
        if (resultPrefab != null)
        {
            if(resultPrefab.GetComponent<Golem>()) canSummonGolem = false;

            // Instantiate the result prefab as a child of a temporary GameObject
            GameObject resultInstance = Instantiate(resultPrefab, resultSlot.position, Quaternion.identity);
            if (resultInstance != null)
            {
                // Retrieve the Item component from the result instance
                Item itemComponent = resultInstance.GetComponent<Item>();

                // Check if the result instance has an Item component
                if (itemComponent != null)
                {
                    // Set the parent of the result instance
                    resultInstance.transform.parent = resultSlot;

                    // Call the PlaceItem method of the Item component
                    itemComponent.PlaceItem(resultSlot);

                    // Call the GetItem method from the pickUp script
                    pickUp.GetItem(itemComponent);
                }
                else
                {

                    //Instantiate(resultPrefab, resultSlot.position, Quaternion.identity);
                }
            }
            else
            {
                // Handle if result prefab instantiation failed
                Debug.LogWarning("Failed to instantiate result prefab.");
            }
        }
        else
        {
            if (dudRecipePrefabs != null && dudRecipePrefabs.Length > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, dudRecipePrefabs.Length);
                Instantiate(dudRecipePrefabs[randomIndex], resultSlot.position, resultSlot.rotation); // Use the stored position
            }
            else
            {
                Debug.Log("No dud recipe prefabs available.");
            }
        }
    }



}
