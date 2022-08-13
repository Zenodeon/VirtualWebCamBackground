using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WebCamOutput : MonoBehaviour
{
    [SerializeField] private SelfieSegmentationController controller;
    [SerializeField] private PlaybackScreen camScreen;
    [SerializeField] private PlaybackScreen humanScreen;
    [Space]
    [SerializeField] public UnityEvent<WebCamTexture> webcamChanged = new UnityEvent<WebCamTexture>(); 
    [SerializeField] public UnityEvent webcamUpdate;
    WebCamTexture webcamFeed;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        GetCamFeed(devices[0].name);
    }

    void Update()
    {
        if (webcamFeed.didUpdateThisFrame)
            webcamUpdate.Invoke();
    }

    public void GetCamFeed(string deviceName)
    {
        webcamFeed = new WebCamTexture(deviceName);
        webcamFeed.Play();

        webcamChanged.Invoke(webcamFeed);
    }
}
