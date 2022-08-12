using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaybackScreen : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Material screenMatPrefab;

    private void Start()
    {
        image.material = new Material(screenMatPrefab);
    }

    public void SetScreenTexture(Texture texture)
    {
        image.material.mainTexture = texture;
    }
}
