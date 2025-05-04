using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
   public static TransitionManager Instance;

   public CanvasGroup canvasGroup;
   public bool isFade;
   public float fadeTime;
   
   private void Awake()
   {
      if(Instance != null)
         Destroy(this);
      else Instance = this;
   }

   public void OnTransition(string currentScene, string nextScene)
   {
      if (!isFade)
      {
         StartCoroutine(TransitionRoutine(currentScene, nextScene));
      }
   }
   
   //场景转换
   private IEnumerator TransitionRoutine(string currentScene, string nextScene)
   {
      isFade = true;
      LoadingEvents.OnRiseBeforeLoading();
      yield return StartCoroutine(Fade(1));
      yield return SceneManager.UnloadSceneAsync(currentScene);
      yield return SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
      
      Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
      SceneManager.SetActiveScene(newScene);
      LoadingEvents.OnRiseAfterLoading();
      yield return StartCoroutine(Fade(0));
      isFade = false;
   }

   //渐变效果
   private IEnumerator Fade(float fadeAlpha)
   {
      canvasGroup.blocksRaycasts = true;
      var speed = Mathf.Abs(canvasGroup.alpha - fadeAlpha)/fadeTime;
      while (!Mathf.Approximately(fadeAlpha, canvasGroup.alpha))
      {
         canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, fadeAlpha, speed*Time.deltaTime);
         yield return null;
      }
      canvasGroup.blocksRaycasts = false;
   }
}
