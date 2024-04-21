using UnityEngine;

public class ItemSpawner : Interactive
{
    PlaySounds sounds;
    public GameObject ItemPrefab;
    public ItemData data;
    Item item;
    public bool isDupper;

    void Awake()
    {
        sounds = GetComponent<PlaySounds>();
    }
    Item SpawnItem()
    {
        return Instantiate(ItemPrefab, transform.position, transform.rotation).GetComponent<Item>();
    }

    public override Item GetItem()
    {
       

        sounds.PlaySpecial();
        if(!canInteract) return null;
        Item itemInSlot = item;

        if (item == null)
        {
            itemInSlot = SpawnItem();
            itemInSlot.UpdateItemData(data);
        }
        else
        {
            item = null; // Reset the item slot to null after retrieving the item
        }

        return itemInSlot;
    }

    public override bool PlaceItem(Item _item)
    {


        if(!canInteract) return false;
        if (item != null || _item == null) return false; // Check if the slot is already occupied or the item is null

        _item.PlaceItem(slot);
        item = _item;
        if(isDupper){
            data = _item.Data;
            Destroy(item.gameObject);
        }
        return true;
    }
}