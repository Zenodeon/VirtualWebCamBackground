using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WebCamOutput : MonoBehaviour
{
    [SerializeField] CamDisplay display;
    [Space]
    [SerializeField] public UnityEvent<Texture> webcamChanged = new UnityEvent<Texture>(); 
    [SerializeField] public UnityEvent webcamUpdate;

    int currentCamIndex = 0;
    int oldCurrentCamIndex = -1;

    WebCamTexture webcamFeed;
    bool webcamLoaded;

    WebCamDevice[] cams;

    void Start()
    {
        cams = WebCamTexture.devices;
        CamDeviceChange();
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

    private void FixedUpdate()
    {
        if (IMGProcesser._instance.processImage)
            if (cams.Length != WebCamTexture.devices.Length)
                UpdateCamDeviceList();

    }

    private void UpdateCamDeviceList()
    {
        cams = WebCamTexture.devices;
        CamDeviceChange();
    }

    public void GetCamFeed(string deviceName)
    {
        if (webcamLoaded)
            webcamFeed.Stop();

        webcamFeed = new WebCamTexture(deviceName);
        webcamFeed.Play();

        oldCurrentCamIndex = currentCamIndex;

        webcamLoaded = false;
    }

    private void CamDeviceChange()
    {
        if (cams.Length == 0)
        {
            display.SetName("No Cam Found");
            return;
        }
        else
            display.ArrowState(cams.Length != 1);

        currentCamIndex = Mathf.Clamp(currentCamIndex, 0, cams.Length - 1);
        DisplayCamIndex();
    }

    private void DisplayCamIndex()
    {
        if (currentCamIndex == oldCurrentCamIndex)
            return;

        string camName = cams[currentCamIndex].name;
        display.SetName(camName);
        GetCamFeed(camName);
    }

    public void UpdateDeltaCamIndex(int delta)
    {
        if(delta == 0)
        {
            oldCurrentCamIndex = -1;
            UpdateCamDeviceList();
            return;
        }

        currentCamIndex += delta;
        currentCamIndex = Mathf.Clamp(currentCamIndex, 0, cams.Length - 1);
        DisplayCamIndex();
    }
}
