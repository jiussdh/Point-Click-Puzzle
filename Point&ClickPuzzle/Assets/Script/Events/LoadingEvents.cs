using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//场景加载事件集合
public class LoadingEvents
{
    
    public static Action BeforeLoadingEvents;

    public static void OnRiseBeforeLoading()
    {
        BeforeLoadingEvents?.Invoke();
    }
    
    public static Action AfterLoadingEvents;

    public static void OnRiseAfterLoading()
    {
        AfterLoadingEvents?.Invoke();
    }
}
