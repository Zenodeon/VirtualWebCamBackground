using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundCrtl : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image foreground;
    [SerializeField] private PlaybackScreen screen;
    [Space]
    [SerializeField] private List<Sprite> bgSprites;

    private int currentBGIndex = 0;

    public void OnImageReady(IMGProcesser imgP)
    {
        screen.SetScreenTexture(imgP.maskedCamFeed);
    }

    public void ChangeBG(int deltaIndex)
    {
        currentBGIndex += deltaIndex;

        if(currentBGIndex > bgSprites.Count - 1)
            currentBGIndex = 0;
        else if(currentBGIndex < 0)
            currentBGIndex = bgSprites.Count - 1;

        background.sprite = bgSprites[currentBGIndex];
    }

    public void ToggleDesk(bool state)
    {
        foreground.enabled = state;
    }
}
