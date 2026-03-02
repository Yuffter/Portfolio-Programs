using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Febucci.UI;
using KanKikuchi.AudioManager;
using TMPro;
using UnityEngine;

public class GameStartAnimation : MonoBehaviour
{
    [SerializeField] RectTransform missionBackground;
    [SerializeField] TypewriterByCharacter missionTypewriter;
    [SerializeField,TextArea(1,15)] string missionText;
    [SerializeField] TMP_Text closeExplanation;
    [SerializeField] RectTransform tutorialCanvas;
    [SerializeField] WaveChangeAnimation waveChangeAnimation;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init() {
        BGMManager.Instance.Stop(BGMPath.GAME);
        missionBackground.localScale = new Vector3(0,4,1);
        closeExplanation.alpha = 0f;
    }

    async Task Play() {
        Sequence seq = DOTween.Sequence();
        await seq.AppendInterval(1f)
            .AppendCallback(() => SEManager.Instance.Play(SEPath.OPEN_MISSION))
            .Append(missionBackground.DOScaleX(4,0.5f).SetEase(Ease.OutQuad)).AsyncWaitForCompletion();
        missionTypewriter.ShowText(missionText);
        await UniTask.WaitUntil(() => missionTypewriter.isShowingText == false);
        await closeExplanation.DOFade(1,1).AsyncWaitForCompletion();
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        seq = DOTween.Sequence();
        await seq.AppendCallback(() => SEManager.Instance.Play(SEPath.DETERMINE))
        .AppendInterval(1f)
        .Append(tutorialCanvas.DOScaleX(0,1f)).AsyncWaitForCompletion();

        // BGMManager.Instance.Play(BGMPath.GAME);
        await waveChangeAnimation.Wave_1();
        GameTimeManager.IsStarting = true;
    }
}
