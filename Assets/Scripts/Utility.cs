using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using DG.Tweening;

public static class Utility
{
    public static void Fade(this CanvasGroup group, bool state)
    {
        if (state)
            group.DOFade(1, 0.3f);
        else
            group.DOFade(0, 0.3f);

        group.interactable = state;
        group.blocksRaycasts = state;
    }
}

