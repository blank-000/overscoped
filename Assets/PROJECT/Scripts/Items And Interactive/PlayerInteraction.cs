using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    
    public bool IsHoldingObject { get; private set; }
    [SerializeField] Transform handSlot;
    Interactive currentInteraction;
    PreparationStation station;
    RecipeBookDisplay recipeDisplay;
    Item currentItem;

    public void RecieveItem(Item item)
    {
        currentItem = item;
        currentItem.PlaceItem(handSlot);
        IsHoldingObject = true;
    }
    

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Interactive>() != currentInteraction)
        {
            currentInteraction = other.GetComponent<Interactive>();
            station = other.GetComponent<PreparationStation>();
            recipeDisplay = other.GetComponent<RecipeBookDisplay>();
            if(recipeDisplay != null) recipeDisplay.TurnON();

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactive>() == currentInteraction)
        {
            if(recipeDisplay != null) recipeDisplay.TurnOFF();
            currentInteraction = null;
            station = null;
            recipeDisplay = null;
        }
    }

    public void UIIgredientProcess()
    {
        if(station != null)
        {
            currentInteraction.PrepareItem();
        }
    }


    public void OnIngedientProccess(InputAction.CallbackContext ctx)
    {
        if(ctx.started && station != null)
        {
            currentInteraction.PrepareItem();
        }
    }

    public void UIInteract()
    {
        if(currentInteraction != null)
        {
            if (!IsHoldingObject)
            {
                currentItem = currentInteraction.GetItem();
                if (currentItem != null)
                {
                    currentItem.PlaceItem(handSlot);

                    IsHoldingObject = true;
                }
            }
            else
            {
                if(currentInteraction.PlaceItem(currentItem))
                {
                    IsHoldingObject = false;
                }
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        // I have decided to have it be automatic when near
        // if(recipeDisplay != null && ctx.started )
        // {
        //     recipeDisplay.ToggleRecipeBook();
        //     return;
        // }

        if (ctx.started && currentInteraction != null)
        {
            if (!IsHoldingObject)
            {
                currentItem = currentInteraction.GetItem();
                if (currentItem != null)
                {
                    currentItem.PlaceItem(handSlot);

                    IsHoldingObject = true;
                }
            }
            else
            {
                if(currentInteraction.PlaceItem(currentItem))
                {
                    IsHoldingObject = false;
                }
            }
        }
    }
}
