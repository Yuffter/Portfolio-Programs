using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using KanKikuchi.AudioManager;

public class PlayerHPEffectView : MonoBehaviour
{
    Volume volume;
    Vignette vignette;
    Sequence sequence;
    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Warn()
    {
        sequence = DOTween.Sequence();
        sequence.Append(DOVirtual.Float(0, 0.4f, 0.5f, x => vignette.intensity.value = x));
        sequence.Append(DOVirtual.Float(0.4f, 0f, 0.5f, x => vignette.intensity.value = x));
        sequence.SetLoops(-1);
        sequence.Play().OnKill(() => {
            sequence.Pause();
            sequence.Kill();
        });
    }

    public void CancelWarn()
    {
        sequence.Pause();
        sequence.Kill();
    }

    public void DeadScreen() {
        DOVirtual.Float(0, 1f, 2f, x => vignette.intensity.value = x);
        BGMManager.Instance.ChangePitch(BGMPath.GAME,2,1,0.5f);
    }
}
