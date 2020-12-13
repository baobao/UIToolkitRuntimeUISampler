using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public static class MyTween
{
    private const float SlideX = 60f;
    public const float Duration = 0.3f;
    private static readonly Ease ShowEase;
    private static readonly Ease HideEase;

    static MyTween()
    {
        HideEase = Ease.OutCubic;
        ShowEase = Ease.OutCubic;
    }

    public static Tween HideX(VisualElement ve, bool isLeft, bool isInstant = false)
    {
        var tr = ve.transform;
        var pos = tr.position;
        pos.x += isLeft ? SlideX : -SlideX;

        if (isInstant)
        {
            InternalAlpha(ve, 0, 0f);
            tr.position = pos;
            return null;
        }

        var seq = DOTween.Sequence();
        seq.Insert(0, DOTween.To(
            () => tr.position,
            x => tr.position = x, pos, Duration).SetEase(HideEase));
        seq.Insert(0, InternalAlpha(ve, Duration, 0f, HideEase));

        return seq;
    }

    public static Tween ShowX(VisualElement ve, float goalX = 0, bool isInstant = false)
    {
        var tr = ve.transform;
        var pos = tr.position;
        pos.x = goalX;

        if (isInstant)
        {
            InternalAlpha(ve, 0, 1f);
            tr.position = pos;
            return null;
        }

        var seq = DOTween.Sequence();
        seq.Insert(0, DOTween.To(
            () => tr.position,
            x => tr.position = x, pos, Duration).SetEase(ShowEase));
        seq.Insert(0, InternalAlpha(ve, Duration, 1f, ShowEase));

        return seq;
    }

    public static Tween ShowAlpha(VisualElement ve, bool isInstant = false, float customDuration = 0,
        Ease customEase = default)
    {
        customDuration = customDuration > 0 ? customDuration : Duration;
        customEase = customEase != default ? customEase : ShowEase;
        return InternalAlpha(ve, isInstant ? 0 : customDuration, 1f, customEase);
    }

    public static Tween HideAlpha(VisualElement ve, bool isInstant = false, float customDuration = 0)
    {
        var duration = customDuration > 0 ? customDuration : Duration;
        return InternalAlpha(ve, isInstant ? 0 : duration, 0, HideEase);
    }

    private static Tween InternalAlpha(VisualElement ve, float duration, float alphaValue, Ease ease = default)
    {
        var style = ve.style;
        if (duration <= 0)
        {
            style.opacity = new StyleFloat(alphaValue);
            return null;
        }

        return DOTween.To(() => ve.resolvedStyle.opacity,
                x => style.opacity = new StyleFloat(x), alphaValue, duration)
            .SetEase(ease);
    }
}