using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : Singleton<UIFade>
{
    [SerializeField] Image fadeScreen;
    [SerializeField] float fadeSpeed = 0.5f;
    IEnumerator fadeRoutine;
    public bool endFading { get; private set;} = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public void FadeToValue(float value)
    {
        endFading = false;
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }
        
        fadeRoutine = FadeRoutine(value);
        StartCoroutine(fadeRoutine);
    }

    IEnumerator FadeRoutine(float targetAlpha)
    {
        while (!Mathf.Approximately(fadeScreen.color.a, targetAlpha))
        {
            float alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);
            yield return null;
        }
        endFading = true;
    }
}
