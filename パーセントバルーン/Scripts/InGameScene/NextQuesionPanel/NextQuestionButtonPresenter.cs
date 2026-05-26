using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using KanKikuchi.AudioManager;
using System;
using UnityEngine.SceneManagement;
using TMPro;

namespace InGameScene {
    public class NextQuestionButtonPresenter : MonoBehaviour
    {
        [SerializeField, Header("アニメーター")]
        private NextQuestionButtonAnimator _nextQuestionButtonAnimator;

        [SerializeField, Header("ビュー")]
        private NextQuestionButtonView _nextQuestionButtonView;

        [SerializeField, Header("終了パネルのCanvasGroup")]
        private CanvasGroup _endPanelCanvasGroup;

        [SerializeField, Header("終了テキスト")]
        private TextMeshProUGUI _endText;

        public IObservable<Unit> OnClickAsObservable => _nextQuestionButtonView.OnClickAsObservable;

        private float _pressingTime = 0f;
        private const float PRESSING_TIME_LIMIT = 3;
        private bool _isFinished = false;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _nextQuestionButtonAnimator.Initialize();

            _nextQuestionButtonView.OnClickAsObservable
                .Subscribe(_ => {
                    SEManager.Instance.Play(SEPath.DECISION_BUTTON);
                    _nextQuestionButtonAnimator.HideNextButton().Forget();
                }).AddTo(this);

                /* 強制終了 */
                Observable.EveryUpdate()
                    .TakeUntilDestroy(this)
                    .Where(_ => Input.GetKeyDown(KeyCode.Tab))
                    .Where(_ => !_isFinished)
                    .Subscribe(_ => {
                        _pressingTime = 0f;
                        _endPanelCanvasGroup.alpha = 1f;
                        _endPanelCanvasGroup.interactable = true;
                        _endPanelCanvasGroup.blocksRaycasts = true;
                        _endText.text = "終了中.";
                    }).AddTo(this);

                Observable.EveryUpdate()
                    .TakeUntilDestroy(this)
                    .Where(_ => Input.GetKey(KeyCode.Tab))
                    .Where(_ => !_isFinished)
                    .Subscribe(_ => {
                        _pressingTime += Time.deltaTime;

                        if (_pressingTime >= 3) {
                            BGMManager.Instance.Stop();
                            FadeManager.Instance.FadeIn(1, () => {
                                SceneManager.LoadScene("GeneralResultScene");
                            });
                            _isFinished = true;
                        }
                        else if (_pressingTime >= 2) {
                            _endText.text = "終了中...";
                        }
                        else if (_pressingTime >= 1) {
                            _endText.text = "終了中..";
                        }
                    }).AddTo(this);

                Observable.EveryUpdate()
                    .TakeUntilDestroy(this)
                    .Where(_ => Input.GetKeyUp(KeyCode.Tab))
                    .Where(_ => !_isFinished)
                    .Subscribe(_ => {
                        _endPanelCanvasGroup.alpha = 0f;
                        _endPanelCanvasGroup.interactable = false;
                        _endPanelCanvasGroup.blocksRaycasts = false;
                    }).AddTo(this);
        }

        public async UniTask ShowNextButton() {
            if (GameData.Instance.CurrentQuestionNumber == GameData.Instance.QuestionsData.Count) {
                _nextQuestionButtonView.ChangeText("結果発表へ");
            }
            await _nextQuestionButtonAnimator.ShowNextButton();
        }
    }
}
