using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class CanvasGroupTimelineBehaviour : PlayableBehaviour
{
    public CanvasGroupTimelineClip clip { get; set; }
    CanvasGroup g = null;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (g != null) return;
        g = playerData as CanvasGroup;

        if (clip.interactable) g.interactable = true;
        else g.interactable = false;

        if (clip.blocksRaycasts) g.blocksRaycasts = true;
        else g.blocksRaycasts = false;
    }
}
