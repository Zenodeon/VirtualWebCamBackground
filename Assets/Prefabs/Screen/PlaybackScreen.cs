using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaybackScreen : MonoBehaviour
{
    [SerializeField] private RawImage image;

    public void SetScreenTexture(Texture texture)
    {
        image.texture = texture;
    }
}
