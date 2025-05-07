using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionFrame : Frame
{
    protected override void OnEnable()
    {
        base.OnEnable();
        FrameEvents.ChangeIntroductionEvent += SetAttribute;
        FrameEvents.ShowIntroductionEvent += ShowFrame;
    }
    
    protected void OnDisable()
    {
        FrameEvents.ChangeIntroductionEvent -= SetAttribute;
        FrameEvents.ShowIntroductionEvent -= ShowFrame;
    }
    
    protected override void SetAttribute(string introduction)
    {
        TextContent = introduction;
        StretchTime = TextContent.Length * stretchTimeForOneWord;
        ShortenTime = TextContent.Length * shortenTimeForOneWord;
    }

    
}
