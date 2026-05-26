using UnityEngine;
using System.Linq;
using InGameScene.Interface;
using KanKikuchi.AudioManager;
using Cysharp.Threading.Tasks;
using UniRx;
using System;
using UnityEngine.SceneManagement;

namespace InGameScene
{
    /// <summary>
    /// パーセントバルーンの全体の流れを管理するクラス
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField, Header("ゲーム開始ボタン")]
        private GameStartButton _gameStartButton;
        [SerializeField, Header("気球の初期アニメーションを行うクラス")]
        private AirshipInitialAnimator _airshipInitialAnimator;
        [SerializeField, Header("解答提出ボタン")]
        private DecisionPercentButton _decisionPercentButton;
        [SerializeField, Header("パーセント入力パネルアニメーター")]
        private PercentInputPanelAnimator _percentInputPanelAnimator;
        [SerializeField, Header("パーセント矢印生成器")]
        private PercentArrowGenerator _percentArrowGenerator;
        [SerializeField, Header("パーセントゲージアニメーター")]
        private PercentGaugeAnimator _percentGaugeAnimator;
        [SerializeField, Header("バルーン破壊アニメーター")]
        private BalloonBreakAnimator _balloonBreakAnimator;
        [SerializeField, Header("気球提供クラス")]
        private AirshipsProvider _airshipsProvider;
        [SerializeField, Header("墜落エフェクト")]
        private GameObject _fallEffectPrefab;
        [SerializeField, Header("爆破エフェクト")]
        private GameObject _explosionEffectPrefab;
        [SerializeField, Header("次の問題へ進むボタンのPresenter")]
        private NextQuestionButtonPresenter _nextQuestionButtonPresenter;
        [SerializeField, Header("問題出題アニメーター")]
        private QuestionAnimator _questionAnimator;

        private void Awake()
        {
            Initialize();
            PlayMainSequence().Forget();
        }

        private void Initialize()
        {
            /* ゲームの初期化処理を行う */
            GameData.Instance.Initialize();

            /* シーンに存在するInitializerを全て取得する */
            var initializers = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IInitializer>();
            foreach (var initializer in initializers)
            {
                initializer.Initialize();
            }

            BGMManager.Instance.Play(BGMPath.IN_GAME_BACKGROUND, 0.6f);
            GameData.Instance.CurrentQuestionNumber = 0;
        }

        private async UniTaskVoid PlayMainSequence()
        {
            /* ゲーム開始ボタンが押されるのを待つ */
            await _gameStartButton.OnClickAsObservable.FirstOrDefault().ToUniTask();

            /* ゲーム開始のアニメーションを再生する */
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            await _airshipInitialAnimator.Animate();
            await UniTask.Delay(TimeSpan.FromSeconds(1));

            for (int i = 0;i < GameData.Instance.QuestionsData.Count;i++)
            {
                /* 問題を出題して、解答パネルを表示するところまで行う */
                await QuestionManager.Instance.AskNextQuestion();

                /* 解答提出ボタンが押されるのを待つ */
                await _decisionPercentButton.OnClickAsObservable.FirstOrDefault().ToUniTask();

                /* 解答提出のアニメーションを再生する */
                _percentInputPanelAnimator.HidePercentInputPanel().Forget();
                SEManager.Instance.Play(SEPath.DON);
                BGMManager.Instance.Pause(BGMPath.IN_GAME_BACKGROUND);
                BGMManager.Instance.Play(BGMPath.LIMIT, 0.3f);
                _percentArrowGenerator.Generate();
                await _percentGaugeAnimator.Animate(GameData.Instance.QuestionsData[GameData.Instance.CurrentQuestionNumber - 1].Percentage);

                /* バルーンを破壊するアニメーションを再生する */
                BalloonBreakCalculator balloonBreakCalculator = new BalloonBreakCalculator();
                await _balloonBreakAnimator.BreakEachGroupBalloons(balloonBreakCalculator.GroupsDifferencePercent);

                /* 墜落アニメーション(存在すれば) */
                AirshipEffectFactory airshipEffectFactory = new AirshipEffectFactory(_explosionEffectPrefab, _fallEffectPrefab);
                AirshipsBreaker airshipsBreaker = new AirshipsBreaker(_airshipsProvider, airshipEffectFactory);
                await airshipsBreaker.Break();

                /* 次の問題へ進むボタンを表示する */
                await _nextQuestionButtonPresenter.ShowNextButton();

                /* 次の問題へ進むボタンが押されるのを待つ */
                await _nextQuestionButtonPresenter.OnClickAsObservable.FirstOrDefault().ToUniTask();

                _questionAnimator.HideTimeCircle().Forget();
                _percentGaugeAnimator.HideGauge().Forget();
                await _percentArrowGenerator.DeleteArrows();
            }

            BGMManager.Instance.Stop();
            FadeManager.Instance.FadeIn(1, () =>
            {
                SceneManager.LoadScene("GeneralResultScene");
            });
        }
    }
}
