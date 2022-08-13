using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WebCamOutput : MonoBehaviour
{
    [SerializeField] public UnityEvent<Texture> webcamChanged = new UnityEvent<Texture>(); 
    [SerializeField] public UnityEvent webcamUpdate;

    WebCamTexture webcamFeed;
    bool webcamLoaded;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        GetCamFeed(devices[0].name);
    }

    void Update()
    {
        if (webcamFeed.didUpdateThisFrame)
        {
            if (webcamLoaded)
                webcamUpdate.Invoke();
            else
            {
                webcamLoaded = true;
                webcamChanged.Invoke(webcamFeed);
            }
        }
    }

    public void GetCamFeed(string deviceName)
    {
        webcamFeed = new WebCamTexture(deviceName);
        webcamFeed.Play();

        webcamLoaded = false;
    }
}
