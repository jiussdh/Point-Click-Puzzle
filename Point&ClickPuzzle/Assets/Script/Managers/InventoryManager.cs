using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    
    public Inventory myBag;
    public GameObject slotGrid;
    public GameObject slotPrefab;
    public List<GameObject> slots = new List<GameObject>();

    public void Awake()
    {
        if(instance != null)
            Destroy(this);
        instance = this;
    }

    public void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        //清空背包
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if(instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            slots.Clear();
        }
        //重新加载物品
        for (int i = 0; i < instance.myBag.inventory.Count; i++)
        {
            instance.slots.Add(Instantiate(instance.slotPrefab));
            instance.slots[i].transform.SetParent(slotGrid.transform);
            instance.slots[i].gameObject.GetComponent<Slot>().SetSlot(myBag.inventory[i],i);
        }
    }
}
