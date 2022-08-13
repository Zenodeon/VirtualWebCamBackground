using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMGProcesser : MonoBehaviour
{
    [SerializeField] private SelfieSegmentationController SSCtrler;
    [Space]
    [Range(0, 1)] public float thresholdLevel = 0.314f;
    [Space]
    public Texture camFeed;
    public Texture2D personMask;
    public Texture2D thresholdedMask;
    public Texture2D maskedCamFeed;
    [Space]
    [SerializeField] PlaybackScreen s1;
    [SerializeField] PlaybackScreen s2;
    [SerializeField] PlaybackScreen s3;
    [SerializeField] PlaybackScreen s4;

    public void OnCamChange(Texture newFeed)
    {
        camFeed = newFeed;
        s1.SetScreenTexture(camFeed);

        thresholdedMask = new Texture2D(camFeed.width, camFeed.height, TextureFormat.RGBA32, false);
        maskedCamFeed = new Texture2D(camFeed.width, camFeed.height, TextureFormat.RGBA32, false);

        ProcessImage();
    }

    public void OnCamUpdate()
    {
        if (!camFeed)
            return;

        ProcessImage();
    }

    private void ProcessImage()
    {
        GetPersonMask();
        CleanMask(personMask);
        MaskedOutCamFeed(thresholdedMask);
    }

    private void GetPersonMask()
    {
        personMask = SSCtrler.GetHuman(camFeed);
        s2.SetScreenTexture(personMask);
    }

    private void CleanMask(Texture2D mask)
    {
        Color[] maskData = mask.GetPixels();

        int dataLength = maskData.Length;
        for (int pi = 0; pi < dataLength; pi++)
        {
            float alpha = maskData[pi].a;
            if (alpha <= thresholdLevel)
                maskData[pi].a = 0;
        }

        thresholdedMask.SetPixels(maskData);
        thresholdedMask.Apply();

        s3.SetScreenTexture(thresholdedMask);
    }

    private void MaskedOutCamFeed(Texture2D mask)
    {
        Texture2D camf = camFeed.ToTexture2D();

        Color[] maskingData = camf.GetPixels();
        Color[] maskData = mask.GetPixels();

        int dataLength = maskingData.Length;
        for (int pi = 0; pi < dataLength; pi++)
            maskingData[pi].a = maskData[pi].a;
        
        maskedCamFeed.SetPixels(maskingData);
        maskedCamFeed.Apply();

        s4.SetScreenTexture(maskedCamFeed);
    }

    private void PrintSize(Texture texture)
    {
        Debug.Log(texture.width + " | " + texture.height);
    }
}
