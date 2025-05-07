using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "item", menuName = "ScriptableObjects/item")]
public class Item : ScriptableObject
{
    public ItemName itemName;
    public Sprite itemImage;
    [TextArea]
    public string itemIntroduction;
}
