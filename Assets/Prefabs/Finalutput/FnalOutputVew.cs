using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Ookii.Dialogs.Wpf;

using DG.Tweening;

public class FnalOutputVew : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private RectTransform maskRect;
    [SerializeField] private UIControllers controller;
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
        string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "/Aswin_Background_Removal";
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        string fileName = "ABR_" + DateTime.Now.ToString("hhmmssff") + ".png";

        string filePath = folder + "/" + fileName;
        byte[] ssData = screenCapture.EncodeToPNG();

        File.WriteAllBytes(filePath, ssData);
    }

    public void Retake()
    {
        maskRect.DOAnchorPosY(0, duration);
        maskRect.DOSizeDelta(new Vector2(1280, 720), duration).OnComplete(() => controller.Retake());
    }
}
