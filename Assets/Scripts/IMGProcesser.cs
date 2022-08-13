using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMGProcesser : MonoBehaviour
{
    [SerializeField] private SelfieSegmentationController SSCtrler;
    [Space]
    public Texture camFeed;
    public Texture personMask;
    public Texture thresholdedMask;
    public Texture maskedCamFeed;
    [Space]
    [SerializeField] PlaybackScreen s1;
    [SerializeField] PlaybackScreen s2;
    [SerializeField] PlaybackScreen s3;
    [SerializeField] PlaybackScreen s4;

    private void Start()
    {
        UpdateScreenTexture();
    }

    public void OnCamChange(Texture newFeed)
    {
        camFeed = newFeed;

        ProcessImage();
        UpdateScreenTexture();
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
        MaskedOutCamFeed(personMask);
    }

    private void GetPersonMask()
    {
        personMask = SSCtrler.GetHuman(camFeed);
    }

    private void CleanMask()
    {

    }

    private void MaskedOutCamFeed(Texture mask)
    {
        maskedCamFeed = new Texture2D(camFeed.width, camFeed.height, TextureFormat.ARGB32, false);
    }

    private void UpdateScreenTexture()
    {
        s1.SetScreenTexture(camFeed);
        s2.SetScreenTexture(personMask);
        s3.SetScreenTexture(thresholdedMask);
        s4.SetScreenTexture(maskedCamFeed);
    }

    private void PrintSize(Texture texture)
    {
        Debug.Log(texture.width + " | " + texture.height);
    }
}
