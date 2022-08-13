using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundCrtl : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image foreground;
    [SerializeField] private PlaybackScreen screen;

    public void OnImageReady(IMGProcesser imgP)
    {
        screen.SetScreenTexture(imgP.maskedCamFeed);
    }
}
