using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IMGProcesser : MonoBehaviour
{
    public static IMGProcesser _instance;

    [SerializeField] private SelfieSegmentationController SSCtrler;
    [Space]
    [SerializeField] [Range(0, 1)] private float thresholdBlackLevel = 0.314f;
    [SerializeField] [Range(0, 1)] private float thresholdWhiteLevel = 0.686f;
    [Space]
    public Texture camFeed;
    public Texture2D personMask;
    public Texture2D thresholdedMask;
    public Texture2D maskedCamFeed;
    [Space]
    public bool processImage = true;
    [Space]
    public UnityEvent<IMGProcesser> OnProcessed = new UnityEvent<IMGProcesser>();

    private void Awake()
    {
        _instance = this;
    }

    public void OnCamChange(Texture newFeed)
    {
        camFeed = newFeed;

        thresholdedMask = new Texture2D(camFeed.width, camFeed.height, TextureFormat.RGBA32, false);
        maskedCamFeed = new Texture2D(camFeed.width, camFeed.height, TextureFormat.RGBA32, false);

        ProcessImage();
    }

    public void OnCamUpdate()
    {
        if (!camFeed)
            return;

        if (processImage)
            ProcessImage();
    }

    private void ProcessImage()
    {
        GetPersonMask();
        CleanMask(personMask);
        MaskedOutCamFeed(thresholdedMask);

        OnProcessed.Invoke(this);
    }

    private void GetPersonMask()
    {
        personMask = SSCtrler.GetHuman(camFeed);
    }

    private void CleanMask(Texture2D mask)
    {
        Color[] maskData = mask.GetPixels();

        int dataLength = maskData.Length;
        for (int pi = 0; pi < dataLength; pi++)
        {
            float alpha = maskData[pi].a;
            if (alpha < thresholdBlackLevel)
                maskData[pi].a = 0;
            else if (alpha > thresholdWhiteLevel)
                maskData[pi].a = 1;
        }

        thresholdedMask.SetPixels(maskData);
        thresholdedMask.Apply();
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
    }

    private void PrintSize(Texture texture)
    {
        Debug.Log(texture.width + " | " + texture.height);
    }
}
