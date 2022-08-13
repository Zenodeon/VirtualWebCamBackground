using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class UIControllers : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;
    [Space]
    [SerializeField] private float duration = 0.3f;

    public void Capture()
    {
        canvasGroup.DOFade(0, duration);
    }
}
