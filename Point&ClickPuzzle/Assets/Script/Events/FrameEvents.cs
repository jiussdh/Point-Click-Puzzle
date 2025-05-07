using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameEvents
{
    //展示物品简介事件
    public static Action ShowIntroductionEvent;

    public static void OnRiseShowIntroduction()
    {
        ShowIntroductionEvent?.Invoke();
    }
    
    //简介框文字传递事件
    public static Action<string> ChangeIntroductionEvent;

    public static void OnRiseChangeIntroduction(string newIntroduction)
    {
        ChangeIntroductionEvent?.Invoke(newIntroduction);
    }
    
    //展示对话事件
    public static Action ShowDialogueEvent;

    public static void OnRiseShowDialogue()
    {
        ShowDialogueEvent?.Invoke();
    }
    
    //对话框文字传递事件
    public static Action<string> ChangeDialogueEvent;

    public static void OnRiseChangeDialogue(string newDialogue)
    {
        ChangeDialogueEvent?.Invoke(newDialogue);
    }
}

