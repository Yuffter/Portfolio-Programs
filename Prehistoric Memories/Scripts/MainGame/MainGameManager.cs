using DG.Tweening;
using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UniRx;
using UniRx.Triggers;

public class MainGameManager : MonoBehaviour
{
    [Header("ゲーム開始時の演出")]
    [SerializeField] PlayableDirector introExplanationOpenTimeline;
    [SerializeField] PlayableDirector introExplanationCloseTimeline;
    [Header("ヘルプの開閉演出")]
    [SerializeField] PlayableDirector helpOpenTimeline;
    [SerializeField] PlayableDirector helpCloseTimeline;
    [Header("生成物リストの開閉演出")]
    [SerializeField] PlayableDirector creatureListOpenTimeline;
    [SerializeField] PlayableDirector creatureListCloseTimeline;
    [SerializeField] Player player;

    [Header("ゲームオーバー")]
    [SerializeField] PlayableDirector gameOverTimeline;

    [Header("キー設定")]
    [SerializeField] KeyCode helpKey;
    [SerializeField] KeyCode creatureListKey;

    [Header("モデル")]
    [SerializeField] RemainingPieceModel remainingPieceModel;

    [Header("トランジション画像")]
    [SerializeField] Texture allPiecesCollectedTexture;

    public bool isHelpPanelOpen = false;
    public IReadOnlyReactiveProperty<bool> IsCreatureListOpen => isCreatureListOpen;
    public ReactiveProperty<bool> isCreatureListOpen = new ReactiveProperty<bool>(false);
    //public bool isCreatureListOpen = false;
    bool canRetry = false;
    bool canDoAnything = true;

    TutorialManager tutorialManager;

    // Start is called before the first frame update
    void Start()
    {
        //全てのピースを集めた場合
        remainingPieceModel.PieceCount
            .Where(value => value == 0)
            .Subscribe(_ =>
            {
                BGMManager.Instance.FadeOut(2);
                FadeManager.Instance.ChangeTexture(allPiecesCollectedTexture);
                FadeManager.Instance.ChangeColor(Color.white);
                DisablePlayerMoving();
                FadeManager.Instance.FadeIn(2f, () => SceneManagerEx.Instance.LoadAndUnloadScene(
                    $"Stage_{StagesData.Instance.currentStageIndex+1}", "PieceCollector"));
            });

        this.UpdateAsObservable()
            .Where(_ => canRetry)
            .Where(_ => Input.GetKeyDown(KeyCode.R))
            .First()
            .Subscribe(_ =>
            {
                SEManager.Instance.Play(SEPath.DECIDE);
                DOVirtual.DelayedCall(1f, () =>
                {
                    FadeManager.Instance.FadeIn(1f, () =>
                    SceneManagerEx.Instance.LoadAndUnloadScene($"Stage_{StagesData.Instance.currentStageIndex + 1}",
                    $"Stage_{StagesData.Instance.currentStageIndex + 1}", () => FadeManager.Instance.FadeOut(1f)));
                });
            });

        tutorialManager = FindAnyObjectByType<TutorialManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canDoAnything) return;
        if (tutorialManager != null && !tutorialManager.canOpenCreatureList) return;

        //ヘルプの開閉
        if (Input.GetKeyDown(helpKey))
        {
            foreach (var c in FindObjectsByType<NewCreatureController>(FindObjectsSortMode.None))
            {
                if (!c.isMaterialize) return;
            }
            //ヘルプを閉じる
            if (isHelpPanelOpen)
            {
                helpOpenTimeline.Stop();
                helpCloseTimeline.Play();
            }
            //ヘルプを開く
            else
            {
                helpCloseTimeline.Stop();
                helpOpenTimeline.Play();
            }

            isHelpPanelOpen = !isHelpPanelOpen;
        }

        //生成物リストの開閉
        if (Input.GetKeyDown(creatureListKey))
        {
            foreach (var c in FindObjectsByType<NewCreatureController>(FindObjectsSortMode.None))
            {
                if (!c.isMaterialize) return;
            }
            if (isCreatureListOpen.Value)
            {
                creatureListOpenTimeline.Stop();
                creatureListCloseTimeline.Play();
            }
            else
            {
                creatureListCloseTimeline.Stop();
                creatureListOpenTimeline.Play();
            }

            isCreatureListOpen.Value = !isCreatureListOpen.Value;
        }
    }

    void ShowIntroExplanation()
    {
        introExplanationOpenTimeline.Play();
    }

    void HideIntroExplanation()
    {
        introExplanationCloseTimeline.Play();
    }

    public void StartControllingIntro()
    {
        StartCoroutine(StartIntroCoroutine());
        
    }

    IEnumerator StartIntroCoroutine()
    {
        ShowIntroExplanation();

        while (!Input.GetKeyDown(KeyCode.Z)) yield return null;
        SEManager.Instance.Play(SEPath.CLOSE);

        DOTween.PlayAll();
        HideIntroExplanation();

        player.EnableMoving();
    }

    public void StartGameOver()
    {
        print("ゲームオーバー");
        BGMManager.Instance.FadeOut();
        StartCoroutine(PlayGameOverAnimation());
    }

    IEnumerator PlayGameOverAnimation()
    {
        canDoAnything = false;
        gameOverTimeline.Play();
        yield return new WaitForSeconds(3f);
        canRetry = true;
    }

    public void DisablePlayerMoving()
    {
        player.DisableMoving();
        DOTween.PauseAll();
    }

    public void EnablePlayerMoving()
    {
        player.EnableMoving();
        DOTween.PlayAll();
    }
}
