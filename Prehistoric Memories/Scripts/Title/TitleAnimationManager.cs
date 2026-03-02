using DG.Tweening;
using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleAnimationManager : MonoBehaviour
{
    [SerializeField] RectTransform upperBorder, lowerBorder;
    TitleInput titleInput;

    readonly Vector2 MAX_BORDER_SIZE = new Vector2(3000, 1000);
    // Start is called before the first frame update
    void Start()
    {
        titleInput = GetComponent<TitleInput>();
        PlayInitialAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayInitialAnimation()
    {
        upperBorder.sizeDelta = MAX_BORDER_SIZE;
        lowerBorder.sizeDelta = MAX_BORDER_SIZE;

        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(1f)
            .Append(upperBorder.DOSizeDelta(new Vector2(3000, 550), 0.5f).SetEase(Ease.OutCubic))
            .Join(lowerBorder.DOSizeDelta(new Vector2(3000, 550), 0.5f).SetEase(Ease.OutCubic))
            .AppendCallback(() => {
                titleInput.canSelect = true;
                BGMManager.Instance.Play(BGMPath.OPENING);
                })
            .Play();
    }

    public void PlaySelectElementAnimation(TitleStateManager.ElementState elementState)
    {
        SEManager.Instance.Play(SEPath.DECIDE);
        Sequence seq = DOTween.Sequence();
        switch (elementState)
        {
            case TitleStateManager.ElementState.START:
                seq.AppendInterval(1f)
                    .Append(upperBorder.DOSizeDelta(MAX_BORDER_SIZE, 0.5f).SetEase(Ease.OutBounce))
                    .Join(lowerBorder.DOSizeDelta(MAX_BORDER_SIZE, 0.5f).SetEase(Ease.OutBounce))
                    .AppendInterval(1f)
                    .AppendCallback(() =>
                    {
                        FadeManager.Instance.FadeIn(3f);
                        BGMManager.Instance.FadeOut(3f);
                    })
                    .AppendInterval(3f)
                    .AppendCallback(() => SceneManagerEx.Instance.LoadAndUnloadScene("Title", "Intro")).Play();
                break;
            case TitleStateManager.ElementState.EXIT:
                break;
        }
        
    }
}
