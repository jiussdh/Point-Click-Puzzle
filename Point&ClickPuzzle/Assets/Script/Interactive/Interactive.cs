using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//解密交互物品的父类
public class Interactive : MonoBehaviour
{
    [Header("目标物品")]
    public ItemName targetItem;
    public bool isDone;

    public void OnCheck(ItemName item)
    {
        if (item == targetItem)
        {
            CorrectClick();
        }
    }

    protected virtual void CorrectClick()
    {
        isDone = true;
    }
    
}
