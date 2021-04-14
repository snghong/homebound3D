﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fader : MonoBehaviour
{
    CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator FadeOut(float time)
    {
        while (canvasGroup.alpha < 0.5)
        {
            canvasGroup.alpha += Time.deltaTime / time;
            yield return null;
        }
    }

    public IEnumerator FadeIn(float time)
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
        }
    }
}