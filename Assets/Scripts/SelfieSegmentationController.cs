using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mediapipe.SelfieSegmentation;

public class SelfieSegmentationController : MonoBehaviour
{
    [SerializeField] private SelfieSegmentationResource ssResource;

    SelfieSegmentation segment;

    void Start()
    {
        segment = new SelfieSegmentation(ssResource);
    }


    private void OnApplicationQuit()
    {
        segment.Dispose();
    }
}
