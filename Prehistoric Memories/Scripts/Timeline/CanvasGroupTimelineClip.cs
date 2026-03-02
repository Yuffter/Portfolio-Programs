using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Playables;
using DG.Tweening;

[Serializable]
public class CanvasGroupTimelineClip : PlayableAsset
{
    public bool interactable = false;
    public bool blocksRaycasts = false;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<CanvasGroupTimelineBehaviour>.Create(graph);
        var behaviour = playable.GetBehaviour();
        behaviour.clip = this;
        return playable;
    }
}
