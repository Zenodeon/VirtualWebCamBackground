using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

using DG.Tweening;

public class CamDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI camName;
    [SerializeField] private CanvasGroup arrowGroup;
    [Space]
    [SerializeField] public UnityEvent<int> ArrowClick = new UnityEvent<int>();

    public void SetName(string name)
    {
        camName.text = name;
    }

    public void ArrowState(bool state)
    {
        //if (state)
        //    arrowGroup.DOFade(1, 0.3f);
        //else
        //    arrowGroup.DOFade(0, 0.3f);

        //arrowGroup.interactable = state;
        //arrowGroup.blocksRaycasts = state;
    }

    public void Arrow(int delta)
    {
        ArrowClick.Invoke(delta);
    }

}
