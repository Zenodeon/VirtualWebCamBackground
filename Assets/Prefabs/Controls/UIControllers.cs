using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DG.Tweening;

public class UIControllers : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private CanvasGroup counterGroup;
    [SerializeField] private CanvasGroup fovGroup;
    [SerializeField] private CanvasGroup ip2pGroup;
    [SerializeField] private CanvasGroup fullScreenCtrlGroup;
    [Space]
    [SerializeField] private BackgroundCrtl bgCtrl;
    [SerializeField] private FnalOutputVew fov;
    [Space]
    [SerializeField] public Toggle phase2;
    [SerializeField] public Toggle ivPhase2;
    [Space]
    [SerializeField] private float duration = 0.3f;

    public void Capture()
    {
        HideControls();

        CountDown();
    }

    public void HideControls()
    {
        bgCtrl.centerPointRect.DOAnchorPosY(-40, 0.3f);

        canvasGroup.DOFade(0, duration);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void ShowControls()
    {
        bgCtrl.centerPointRect.DOAnchorPosY(10, 0.3f);

        canvasGroup.DOFade(1, duration);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void CountDown()
    {
        counterGroup.DOFade(1, 0.3f);

        fullScreenCtrlGroup.Fade(false);

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
        fullScreenCtrlGroup.Fade(true);

        fovGroup.DOFade(0, duration);

        fovGroup.interactable = false;
        fovGroup.blocksRaycasts = false;

        IMGProcesser._instance.processImage = true;

        ShowControls();
    }

    public void Phase2Mode(bool state)
    {
        ip2pGroup.Fade(state);
    }
}
