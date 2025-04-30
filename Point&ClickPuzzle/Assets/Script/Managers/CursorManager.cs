using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private Vector3 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    private bool _canClick;

    
    private void Update()
    {
        _canClick = ObjectOnCollider();
        if (_canClick && Input.GetMouseButtonDown(0))
        {
            ClickAction(ObjectOnCollider().gameObject);
        }
    }

    private void ClickAction(GameObject clickedObject)
    {
        switch (clickedObject.tag)
        {
            case "Item":
                clickedObject.GetComponent<ItemDetails>().ItemClicked();
                break;
        }
    }

    private Collider2D ObjectOnCollider()
    {
        return Physics2D.OverlapPoint(MousePosition);
    }
}
