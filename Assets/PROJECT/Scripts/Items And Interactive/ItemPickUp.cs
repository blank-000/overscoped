using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    Item currentItem;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!other.GetComponent<PlayerInteraction>().IsHoldingObject && currentItem != null)
            {
                other.GetComponent<PlayerInteraction>().RecieveItem(currentItem);
                currentItem = null;
            }
        }
    }
    public void GetItem(Item item){
        if(currentItem != null){
            Destroy(item.gameObject);
            return;
        }    
        currentItem = item;
    }

    Item ReleaseItem()
    {
        if(currentItem != null)
        {
            return currentItem;
        }else{
            
            return null;

        }
    }
}
