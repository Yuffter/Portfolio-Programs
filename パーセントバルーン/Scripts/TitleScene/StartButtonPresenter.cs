using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using KanKikuchi.AudioManager;

namespace TitleScene {
    public class StartButtonPresenter : MonoBehaviour
    {
        [SerializeField]
        private StartButtonView _startButtonView;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _startButtonView.OnClickAsObservable
                .First()
                .Subscribe(_ => {
                    SEManager.Instance.Play(SEPath.TITLE_START_BUTTON);
                    _startButtonView.StartClickAnimation();
                    _startButtonView.ShowGroupSetting().Forget();
                }).AddTo(this);
        }
    }
}