using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //字典存储场景中物体的数据
    private Dictionary<ItemName,bool> _itemAvailableDict = new Dictionary<ItemName, bool>();
    private Dictionary<string, bool> _interactiveDict = new Dictionary<string, bool>();

    private void OnEnable()
    {
        LoadingEvents.BeforeLoadingEvents += BeforeLoading;
        LoadingEvents.AfterLoadingEvents += AfterLoading;

        InteractiveEvents.PickUpEvents += PickUpItem;
    }

    private void OnDisable()
    {
        LoadingEvents.BeforeLoadingEvents -= BeforeLoading;
        LoadingEvents.AfterLoadingEvents -= AfterLoading;
        
        InteractiveEvents.PickUpEvents -= PickUpItem;
    }

    //场景加载前，添加当前场景的物品数据
    private void BeforeLoading()
    {
        foreach (var vItem in FindObjectsOfType<ItemDetails>())
        {
            if (!_itemAvailableDict.ContainsKey(vItem.thisItem.itemName)) 
                _itemAvailableDict.Add(vItem.thisItem.itemName, true);
        }

        foreach (var vInteractive in FindObjectsOfType<Interactive>())
        {
            if(!_interactiveDict.ContainsKey(vInteractive.name))
                _interactiveDict.Add(vInteractive.name, vInteractive.isDone);
        }
    }

    //场景加载完成，更新目标场景中的物品数据
    private void AfterLoading()
    {
        foreach (var vItem in FindObjectsOfType<ItemDetails>())
        {
            if (!_itemAvailableDict.ContainsKey(vItem.thisItem.itemName)) 
                _itemAvailableDict.Add(vItem.thisItem.itemName, true);
            else vItem.GameObject().SetActive(_itemAvailableDict[vItem.thisItem.itemName]);
        }

        foreach (var vInteractive in FindObjectsOfType<Interactive>())
        {
            if(!_interactiveDict.ContainsKey(vInteractive.name))
                _interactiveDict.Add(vInteractive.name, vInteractive.isDone);
            else 
                vInteractive.isDone = _interactiveDict[vInteractive.name];
        }
    }

    //更新物品状态
    public void PickUpItem(ItemName itemName)
    {
        if(_itemAvailableDict.ContainsKey(itemName))
            _itemAvailableDict[itemName] = false;
    }
}
