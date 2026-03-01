using UnityEngine;
using R3;
using AudioManager.SE;

namespace StageSelect.Connector
{
    public class StageChangeConnector : MonoBehaviour
    {
        [SerializeField, Header("ステージ切り替えの左ボタン")]
        private StageSelect.Button.ButtonClickObserver _leftArrowButton;

        [SerializeField, Header("ステージ切り替えの右ボタン")]
        private StageSelect.Button.ButtonClickObserver _rightArrowButton;

        private CompositeDisposable _disposables = new CompositeDisposable();

        /// <summary>
        /// コネクションを起動する
        /// </summary>
        /// <param name="facade"></param>
        /// <param name="disposables"></param>
        public void Boot(StageSelect.StagePanel.Manager facade, CompositeDisposable disposables)
        {
            _disposables = disposables;
            // 左矢印が押されたらパネルを左にずらす
            _leftArrowButton.OnClick
            .Subscribe(_ =>
            {
                facade.MoveLeft();
                Debug.Log("左矢印が押された");
                SEManager.Instance.Play(SEName.StageSelect);
            }).AddTo(_disposables);

            // 右矢印が押されたらパネルを右にずらす
            _rightArrowButton.OnClick
            .Subscribe(_ =>
            {
                facade.MoveRight();
                Debug.Log("右矢印が押された");
                SEManager.Instance.Play(SEName.StageSelect);
            }).AddTo(_disposables);
        }
    }
}
