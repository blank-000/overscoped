using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeBookDisplay : Interactive
{

    public bool displayed {get ; private set;}
    [SerializeField] GameObject _UI;
    [SerializeField] EventSystem UIsys;
    
    [SerializeField] GameObject[] interactions;

    GameObject currentInteraction;
    int interationIndex;
    void Awake()
    {
        // UIsys = GetComponentInChildren<EventSystem>();
        UIsys = FindFirstObjectByType<EventSystem>();
    }

    public override Item GetItem()
    {
        return null;
    }

    public override bool PlaceItem(Item item)
    {
        return false;
    }

    public void TurnON()
    {
        displayed = true;
        _UI.SetActive(displayed);
        if(interactions.Length <= interationIndex)
        {
            interationIndex = 0;
        }
        
        currentInteraction = interactions[interationIndex];
        foreach(GameObject obj in interactions)
        {
            if (obj != currentInteraction) obj.SetActive(false);
            else 
            {
                obj.SetActive(true);
                foreach(Transform child in obj.transform)
                {
                    if (child.gameObject.CompareTag("Selected"))
                    {
                        child.gameObject.SetActive(true);
                        UIsys.SetSelectedGameObject(child.gameObject);
                    }
                }
            
            }
        }

        //implement displaying of learned recipes
    }
    public void TurnOFF()
    {
        displayed = false;
        _UI.SetActive(displayed);
    }



    public void NextInteraction()
    {
        interationIndex += 1;


    }

}
