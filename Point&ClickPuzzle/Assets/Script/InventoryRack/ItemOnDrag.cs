using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private Transform _originalParent;
    public int _currentID;
    public Inventory myBag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalParent = transform.parent;
        _currentID = GetComponentInParent<Slot>().slotID;
        transform.SetParent(transform.parent.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            //检测到物品交换位置
            if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage")
            {
                var changeItem = eventData.pointerCurrentRaycast.gameObject.transform.parent;
                var changeItemID = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID;
                //实现图片交换
                transform.SetParent(changeItem.parent);
                transform.position = changeItem.parent.position;
                changeItem.SetParent(_originalParent);
                changeItem.position = _originalParent.position;
                //实现数据交换
                (myBag.inventory[_currentID], myBag.inventory[changeItemID]) = (myBag.inventory[changeItemID], myBag.inventory[_currentID]);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }

            //检测到空Slot直接插入
            if (eventData.pointerCurrentRaycast.gameObject.name == "Slot(Clone)")
            {
                var changeSlot = eventData.pointerCurrentRaycast.gameObject.transform;
                var changeItemID = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID;
                transform.SetParent(changeSlot);
                transform.position = changeSlot.position;
                (myBag.inventory[_currentID], myBag.inventory[changeItemID]) = (myBag.inventory[changeItemID], myBag.inventory[_currentID]);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
        }
        
        //其余情况返回原位
        transform.SetParent(_originalParent);
        transform.position = _originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
