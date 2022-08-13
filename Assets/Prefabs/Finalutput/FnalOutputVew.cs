using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class FnalOutputVew : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private RectTransform maskRect;
    [Space]
    [SerializeField] private float yPos;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private float duration;

    private Texture2D screenCapture;

    public void Capture()
    {
        StartCoroutine(CaptureScreenShot(CaptureReady));
    }

    private void CaptureReady()
    {
        rawImage.texture = screenCapture;

        maskRect.DOAnchorPosY(yPos, duration).SetDelay(duration);
        maskRect.DOSizeDelta(new Vector2(width, height), duration).SetDelay(duration);
    }

    IEnumerator CaptureScreenShot(System.Action callback)
    {
        yield return new WaitForEndOfFrame();
        screenCapture = ScreenCapture.CaptureScreenshotAsTexture(2);
        callback();
    }

    public void SaveCapture()
    {
        byte[] ssData = screenCapture.EncodeToPNG();
        
    }

    public void Retake()
    {

    }
}
