using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamOutput : MonoBehaviour
{
    [SerializeField] private PlaybackScreen screen; 

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        foreach(WebCamDevice device in devices)
            Debug.Log(device.name);
        WebCamTexture webcam = new WebCamTexture(devices[0].name);
        webcam.Play();
        screen.SetScreenTexture(webcam);
    }

    void Update()
    {
        
    }
}
