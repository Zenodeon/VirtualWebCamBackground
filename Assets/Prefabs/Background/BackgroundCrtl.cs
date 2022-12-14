using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class BackgroundCrtl : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private RectTransform bgRectTransform;
    [SerializeField] public RectTransform centerPointRect;
    [SerializeField] private UIControllers controler;
    [Space]
    [SerializeField] private Image background;
    [SerializeField] private Image backgroundLeft;
    [SerializeField] private Image backgroundRight;
    [SerializeField] private Image foreground;
    [Space]
    [SerializeField] private PlaybackScreen screen1;
    [SerializeField] private PlaybackScreen screen2;
    [SerializeField] private PlaybackScreen screen3;
    [SerializeField] private PlaybackScreen screen;
    [Space]
    [SerializeField] private List<Sprite> bgSprites;

    private int currentBGIndex = 0;

    private bool fourViewBool = false;

    public int targetCenterPoint = 0;

    public void OnImageReady(IMGProcesser imgP)
    {
        centerPointRect.DOAnchorPosX(imgP.centerPoint, 100f).SetSpeedBased(true);

        if (controler.phase2.isOn)
        {
            float targetPoint = imgP.centerPoint;
            if (controler.ivPhase2.isOn)
                targetPoint = RangedMapClamp(targetPoint, 0, 1280, 1280, 0);

            bgRectTransform.DOAnchorPosX(targetPoint - 640, 100f).SetSpeedBased(true);
        }

        screen.SetScreenTexture(imgP.maskedCamFeed);

        if (fourViewBool)
        {
            screen1.SetScreenTexture(imgP.camFeed);
            screen2.SetScreenTexture(imgP.personMask);
            screen3.SetScreenTexture(imgP.thresholdedMask);
        }
    }

    public void ChangeBG(int deltaIndex)
    {
        currentBGIndex += deltaIndex;

        if(currentBGIndex > bgSprites.Count - 1)
            currentBGIndex = 0;
        else if(currentBGIndex < 0)
            currentBGIndex = bgSprites.Count - 1;

        background.sprite = bgSprites[currentBGIndex];
        backgroundLeft.sprite = bgSprites[currentBGIndex];
        backgroundRight.sprite = bgSprites[currentBGIndex];
    }

    public void ToggleDesk(bool state)
    {
        foreground.enabled = state;
    }

    public void FourView()
    {
        fourViewBool = !fourViewBool;

        if(fourViewBool)
        {
            controler.HideControls();
            rectTransform.DOSizeDelta(new Vector2(1280 * 0.5f, 720 * 0.5f), 0.5f);
        }
        else
        {
            rectTransform.DOSizeDelta(new Vector2(1280, 720), 0.5f);
            controler.ShowControls();
        }
    }
    public float RangedMapClamp(float value, float InMinimum, float InMaximum, float OutMinimum, float OutMaximum)
    {
        var InRange = InMaximum - InMinimum;
        var OutRange = OutMaximum - OutMinimum;
        var finalValue = ((value - InMinimum) * OutRange / InRange) + OutMinimum;

        return finalValue;
    }
}
