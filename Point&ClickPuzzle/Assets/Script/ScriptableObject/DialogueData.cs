using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Dialogue")]
public class DialogueData : ScriptableObject
{
    public List<string> dialogue = new List<string>();
}
