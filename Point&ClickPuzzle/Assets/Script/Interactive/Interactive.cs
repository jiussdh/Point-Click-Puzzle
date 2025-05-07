using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//解密交互物品的父类
public class Interactive : MonoBehaviour
{
    public DialogueData dialogueDataIdle;
    public DialogueData dialogueDataFinish;
    
    private Queue<string> _dialogueQueueIdle;
    private Queue<string> _dialogueQueueFinish;
    private string _currentDialogue;
    [Header("目标物品")]
    public ItemName targetItem;
    public bool isDone;

    private void OnEnable()
    {
        FillDialogue();
    }
    
    public void OnCheck(ItemName item)
    {
        if (item == targetItem)
        {
            CorrectClick();
        }
        else
        {
            EmptyClick();
        }
    }

    protected virtual void EmptyClick()
    {
        GetDialogue(_dialogueQueueIdle);
    }
    
    protected virtual void CorrectClick()
    {
        isDone = true;
        GetDialogue(_dialogueQueueFinish);
    }

    private void FillDialogue()
    {
        _dialogueQueueIdle = new Queue<string>();
        _dialogueQueueFinish = new Queue<string>();

        foreach (var dialogue in dialogueDataIdle.dialogue)
        {
            _dialogueQueueIdle.Enqueue(dialogue);
        }

        foreach (var dialogue in dialogueDataFinish.dialogue)
        {
            _dialogueQueueFinish.Enqueue(dialogue);
        }
    }

    private void GetDialogue(Queue<string> dialogue)
    {
        if (dialogue.Count != 0)
        {
            _currentDialogue = dialogue.Dequeue();
            FrameEvents.OnRiseChangeDialogue(_currentDialogue);
            FrameEvents.OnRiseShowDialogue();
        }
        else
        {
            FillDialogue();
        }
    }
}
