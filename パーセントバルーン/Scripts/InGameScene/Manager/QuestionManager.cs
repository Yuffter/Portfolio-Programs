using Cysharp.Threading.Tasks;
using UnityEngine;
using System;
using KanKikuchi.AudioManager;
using UniRx;
using System.Threading;

namespace InGameScene {
    public class QuestionManager : SingletonBase<QuestionManager>
    {
        [SerializeField, Header("問題出題アニメーション")]
        private QuestionAnimator _questionAnimator;

        [SerializeField, Header("パーセント入力パネルアニメーター")]
        private PercentInputPanelAnimator _percentInputPanelAnimator;

        private CancellationToken _cts;

        protected override void Awake()
        {
            base.Awake();
            _cts = this.GetCancellationTokenOnDestroy();
        }

        /// <summary>
        /// 次の問題を出題する
        /// </summary>
        /// <returns></returns>
        public async UniTask AskNextQuestion() {
            /* 次の問題番号に変更 */
            GameData.Instance.CurrentQuestionNumber++;

            /* 出題アニメーションを再生 */
            await _questionAnimator.AnimateNextQuestion();

            /* 思考時間を与える */
            await UniTask.Delay(TimeSpan.FromSeconds(GameData.Instance.MaxThinkingTime), cancellationToken: _cts);

            SEManager.Instance.Play(SEPath.FINISH_THINKING_TIME);
            /* 入力パネルを表示する */
            await _percentInputPanelAnimator.ShowPercentInputPanel();
        }
    }
}