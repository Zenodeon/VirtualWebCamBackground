using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamOutput : MonoBehaviour
{
    [SerializeField] private SelfieSegmentationController controller;
    [SerializeField] private PlaybackScreen camScreen;
    [SerializeField] private PlaybackScreen humanScreen;

    WebCamTexture webcam;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        foreach(WebCamDevice device in devices)
            Debug.Log(device.name);

        webcam = new WebCamTexture(devices[0].name);
        webcam.Play();
        camScreen.SetScreenTexture(webcam);
    }

    void Update()
    {
        if(webcam.didUpdateThisFrame)
        {
            humanScreen.SetScreenTexture(controller.GetHuman(webcam));
        }
    }
}
