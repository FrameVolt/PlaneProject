using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class FadeInOut {


    public static IEnumerator FadeImage(Image target, float duration, Color color, bool isUnscaleTime)
    {
        if (target == null)
            yield break;

        float alpha = target.color.a;

        for (float t = 0.0f; t < 1.0f; )
        {
            if (target == null)
                yield break;
            Color targetColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha, color.a, t));
            target.color = targetColor;
            yield return null;
            if(isUnscaleTime)
                t += Time.unscaledDeltaTime / duration;
            else
                t += Time.deltaTime / duration;
        }
        target.color = color;
    }

    public static IEnumerator FadeSprite(SpriteRenderer target, float duration, Color color, bool isUnscaleTime)
    {
        if (target == null)
            yield break;

        float alpha = target.material.color.a;

        float t = 0f;
        while (t < 1.0f)
        {
            if (target == null)
                yield break;

            Color targetColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha, color.a, t));
            target.material.color = targetColor;
            if (isUnscaleTime)
                t += Time.unscaledDeltaTime / duration;
            else
                t += Time.deltaTime / duration;
            yield return null;

        }
        target.material.color = color;
    }
}
