using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using DG.Tweening;

public class UIControllers : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private CanvasGroup counterGroup;
    [SerializeField] private CanvasGroup fovGroup;
    [Space]
    [SerializeField] private FnalOutputVew fov;
    [Space]
    [SerializeField] private float duration = 0.3f;

    public void Capture()
    {
        canvasGroup.DOFade(0, duration);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        CountDown();
    }

    private void CountDown()
    {
        counterGroup.DOFade(1, 0.3f);

        float count = 3.5f;
        DOTween.To(() => count, x => count = x, 1, 2.5f).SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                counter.text = (int)count + "";
            })
            .OnComplete(() => counterGroup.DOFade(0, 0.3f).OnComplete(CaptureScreen));
    }

    private void CaptureScreen()
    {
        IMGProcesser._instance.processImage = false;

        fovGroup.DOFade(1, duration);
        fovGroup.interactable = true;
        fovGroup.blocksRaycasts = true;

        fov.Capture();
    }

    public void Retake()
    {
        fovGroup.DOFade(0, duration);

        fovGroup.interactable = false;
        fovGroup.blocksRaycasts = false;

        IMGProcesser._instance.processImage = true;

        canvasGroup.DOFade(1, duration);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}
