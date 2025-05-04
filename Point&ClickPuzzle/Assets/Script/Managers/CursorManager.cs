using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private Vector3 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    private bool _canClick;
    
    private ItemName _currentItemName;

    private void OnEnable()
    {
        InteractiveEvents.PitchOnEvents += SetCurrentItem;
    }

    private void OnDisable()
    {
        InteractiveEvents.PitchOnEvents -= SetCurrentItem;
    }


    private void Update()
    {
        _canClick = ObjectOnCollider();
        if (_canClick && Input.GetMouseButtonDown(0))
        {
            ClickAction(ObjectOnCollider().gameObject);
        }
    }

    //通过检测到物体的tag类型触发对应事件
    private void ClickAction(GameObject clickedObject)
    {
        switch (clickedObject.tag)
        {
            case "Item":
                clickedObject.GetComponent<ItemDetails>().ItemClicked();
                break;
            case "Teleport":
                clickedObject.GetComponent<Teleport>().TeleportToScene();
                break;
            case "Interactive":
                clickedObject.GetComponent<Interactive>().OnCheck(_currentItemName);
                break;
        }
    }
    
    //鼠标检测函数（射线检测）
    private Collider2D ObjectOnCollider()
    {
        return Physics2D.OverlapPoint(MousePosition);
    }

    private void SetCurrentItem(ItemName itemName)
    {
        _currentItemName = itemName;
    }
}
