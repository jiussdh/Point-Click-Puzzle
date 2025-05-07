using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//物品交互功能的事件集合
public class InteractiveEvents 
{
    //拾取物品事件
    public static Action<ItemName> PickUpEvents;

    public static void OnRisePickUp(ItemName itemName)
    {
        PickUpEvents?.Invoke(itemName);
    }
    
    //背包物品选中事件
    public static Action<ItemName> PitchOnEvents;

    public static void OnRisePitch(ItemName itemName)
    {
        PitchOnEvents?.Invoke(itemName);
    }
    
   
}
