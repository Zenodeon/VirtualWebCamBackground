using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaybackScreen : MonoBehaviour
{
    public Image image;

    public void SetScreenTexture(Texture texture)
    {
        image.material.mainTexture = texture;
    }
}
