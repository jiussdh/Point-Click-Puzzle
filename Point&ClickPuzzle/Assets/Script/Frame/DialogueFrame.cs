using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogueFrame : Frame
{
    protected override void OnEnable()
    {
        base.OnEnable();
        FrameEvents.ChangeDialogueEvent += SetAttribute;
        FrameEvents.ShowDialogueEvent += ShowFrame;
    }
    
    protected void OnDisable()
    {
        FrameEvents.ChangeDialogueEvent -= SetAttribute;
        FrameEvents.ShowDialogueEvent -= ShowFrame;
    }
    
    protected override void SetAttribute(string introduction)
    {
        TextContent = introduction;
        StretchTime = TextContent.Length * stretchTimeForOneWord;
        ShortenTime = TextContent.Length * shortenTimeForOneWord;
    }
}
