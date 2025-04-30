using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image slotImage;
    public int slotID;

    public GameObject itemInSlot;
    public void SetSlot(Item item , int id)
    {
        slotID = id;
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }
        slotImage.sprite = item.itemImage;
        itemInSlot.SetActive(true);
    }
    
}
