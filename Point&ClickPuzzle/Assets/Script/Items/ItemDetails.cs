using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetails : MonoBehaviour
{
    public Item thisItem;
    public Inventory myBag;
    
    public void ItemClicked()
    {
        for (int i = 0; i < myBag.inventory.Count; i++)
        {
            if (myBag.inventory[i] == null)
            {
                myBag.inventory[i] = thisItem;
                break;
            }
        }
        InteractiveEvents.OnRisePickUp(thisItem.itemName);
        InventoryManager.instance.Refresh();
        gameObject.SetActive(false);
    }

    
}
