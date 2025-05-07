using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image slotImage;
    public ItemName slotItemName;
    public string slotText;
    public int slotID;

    public GameObject itemInSlot;
    public void SetSlot(Item item , int id)
    {
        slotID = id;
        if (item == null)
        {
            itemInSlot.SetActive(false);
            slotItemName = ItemName.Null;
            return;
        }
        slotItemName = item.itemName;
        slotImage.sprite = item.itemImage;
        slotText = item.itemIntroduction;
        itemInSlot.SetActive(true);
    }
    
    public void ItemPitchOn()
    {
        InteractiveEvents.OnRisePitch(slotItemName);
        FrameEvents.OnRiseChangeIntroduction(slotText);
        FrameEvents.OnRiseShowIntroduction();
    }
}
