using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparationStation : MonoBehaviour
{
    public void ProccessItem(Item _item)

    {
        if(_item != null)
        {
            _item.UpdateItemData(_item.Data.proccesedForm);
        }
    }
}
