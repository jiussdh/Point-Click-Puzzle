using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public string currentScene;
    public string nextScene;

    public void OnEnable()
    {
        if (nextScene == string.Empty)
        {
            gameObject.SetActive(false);
        }
        else gameObject.SetActive(true);
    }

    public void TeleportToScene()
    {
        TransitionManager.Instance.OnTransition(currentScene, nextScene);
    }
}
