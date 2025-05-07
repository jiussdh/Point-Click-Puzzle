using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Frame : MonoBehaviour
{
    public ContentSizeFitter _contentSizeFitter;
    public VerticalLayoutGroup _verticalLayoutGroup;
    public RectTransform frameTransform;  //文本框
    public float fontSize;  //文字大小
    public float horizontalOffset;  //文本框拉伸的偏移值
    public float stretchTimeForOneWord;  //单个文字对应伸长时间
    public float shortenTimeForOneWord;  //单个文字对应缩短时间
    public float fadeTime;  //淡入淡出时间
    public float displayTime;  //展示时间
    public float hideTime;  //文本框消失时间
    public TMP_Text introductionText;
    
    protected float StretchTime;  //拉伸时间
    protected float ShortenTime;  //缩短时间
    protected string TextContent;
    private Vector2 _currentSize;
    private Vector2 _targetSize;
    private float _displayTimeCurrent;
    public CanvasGroup _canvasGroup;
    private bool _isShown ;  //展示状态，通过该状态实现文本定时消失，文本展示时点击其他物品进行切换
    private bool _isDisplayed;
    private Coroutine _displayCoroutine;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _contentSizeFitter = GetComponent<ContentSizeFitter>();
        _verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
    }
    protected virtual void OnEnable()
    {
        //由于初始文字框大小与Vertical Layout Group中的padding值有关，但子物体关闭时无法正常获取初始值，所以通过下面操作设置初始值
        _currentSize = new Vector2(_verticalLayoutGroup.padding.left + _verticalLayoutGroup.padding.right, _verticalLayoutGroup.padding.top + _verticalLayoutGroup.padding.bottom);
    }

    protected virtual void Update()
    {
        if (_isShown&&!_isDisplayed)
        {
            TimeCounter();
        }
    }

    protected void ShowFrame()
    {
        if(_isDisplayed) return;
        if (_displayCoroutine != null)
        {
            StopCoroutine(_displayCoroutine);
        }
        _displayCoroutine = StartCoroutine(_isShown ? AfterHideDisplay() : Display());
    }

    
    private IEnumerator Display()
    {
        _isDisplayed = true;
        _displayTimeCurrent = displayTime;
        _targetSize = _currentSize + new Vector2(fontSize * TextContent.Length, fontSize);
        StartCoroutine(FrameFade(1));
        var stretchSpeed = Mathf.Abs(_currentSize.x - _targetSize.x + horizontalOffset) / StretchTime;
        _contentSizeFitter.enabled = false;
        //文字框拉长
        while (!Mathf.Approximately(frameTransform.sizeDelta.x, _targetSize.x + horizontalOffset))
        {
            float newX = Mathf.MoveTowards(frameTransform.sizeDelta.x, _targetSize.x + horizontalOffset, stretchSpeed * Time.deltaTime);
            float newY = Mathf.MoveTowards(frameTransform.sizeDelta.y, _targetSize.y , stretchSpeed * Time.deltaTime);  
            frameTransform.sizeDelta = new Vector2(newX, newY);
            yield return null;
        }
        //文字淡入 
        introductionText.gameObject.SetActive(true);
        ChangeText(TextContent);
        StartCoroutine(TextFade(1));
        //文字框缩短
        var shortenSpeed = Mathf.Abs(_currentSize.x - _targetSize.x) / ShortenTime;
        while (!Mathf.Approximately(frameTransform.sizeDelta.x, _targetSize.x))
        {
            float newX = Mathf.MoveTowards(frameTransform.sizeDelta.x, _targetSize.x, shortenSpeed * Time.deltaTime);
            frameTransform.sizeDelta = new Vector2(newX, frameTransform.sizeDelta.y);
            yield return null;
        }
        _contentSizeFitter.enabled = true;
        _isShown = true;
        _isDisplayed = false;
    }

    //文字框淡入淡出
    private IEnumerator FrameFade(float fadeAlpha)
    {
        float startAlpha = _canvasGroup.alpha;
        float elapsed = 0f;
    
        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(startAlpha, fadeAlpha, elapsed / fadeTime);
            yield return null;
        }
    
        // 确保最终值精确
        _canvasGroup.alpha = fadeAlpha;
    }
    
    //文字淡入淡出
    private IEnumerator TextFade(float fadeAlpha)
    {
        
        float startAlpha = introductionText.alpha;
        float elapsed = 0f;
        while (elapsed <= StretchTime)
        {
            elapsed += Time.deltaTime;
            introductionText.alpha = Mathf.Lerp(startAlpha, fadeAlpha, elapsed/StretchTime);
            yield return null;
        }
        introductionText.alpha = fadeAlpha;
    }
    
    //计时器
    private void TimeCounter()
    {
        _displayTimeCurrent -= Time.deltaTime;
        if (_displayTimeCurrent <= 0)
        {
            StartCoroutine(Hide());
            _displayTimeCurrent = displayTime;
        }
    }

    //文本消失
    private IEnumerator Hide()
    {
        _isDisplayed = true;
        //文字淡出
        StartCoroutine(TextFade(0));
        introductionText.gameObject.SetActive(false);
        //文字框缩短并淡出
        _contentSizeFitter.enabled = false;
        _targetSize = _currentSize;
        var shortenSpeed = Mathf.Abs(frameTransform.sizeDelta.x - _targetSize.x) / hideTime;
        StartCoroutine(FrameFade(0));
        while (!Mathf.Approximately(frameTransform.sizeDelta.x, _targetSize.x))
        {
            float newX = Mathf.MoveTowards(frameTransform.sizeDelta.x, _targetSize.x, shortenSpeed * Time.deltaTime);
            if (frameTransform.sizeDelta.x <= frameTransform.sizeDelta.y)
            {
                float newY = Mathf.MoveTowards(frameTransform.sizeDelta.y, _targetSize.y , shortenSpeed * Time.deltaTime);
                frameTransform.sizeDelta = new Vector2(newX, newY);
            }
            else
            {
                frameTransform.sizeDelta = new Vector2(newX, frameTransform.sizeDelta.y);
            }
            yield return null;
        }
        _isDisplayed = false;
        _isShown = false;
    }

    //确保先消失在出现，避免预期外错误
    private IEnumerator AfterHideDisplay()
    {
        yield return StartCoroutine(Hide());
        yield return StartCoroutine(Display());
    }

    //修改文字
    private void ChangeText(string text)
    {
        introductionText.text = text;
    }

    //给根据文字动态改变的数据赋值
    protected virtual void SetAttribute(string introduction)
    {
        
    }
}
