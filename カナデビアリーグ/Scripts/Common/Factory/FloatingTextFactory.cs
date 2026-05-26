using MinutesGame.Common.Interfaces;
using UnityEngine;
using VContainer;

namespace MinutesGame.Common.Factory
{
    public class FloatingTextFactory : MonoBehaviour, Common.Interfaces.IFactory<Common.FloatingText.FloatingTextController>
    {
        [SerializeField]
        private Common.FloatingText.FloatingTextController _floatingTextControllerPrefab;

        [SerializeField]
        private Common.SO.FloatingTextSetting _floatingTextSetting;

        [SerializeField]
        private Transform _canvasTransform;

        /// <summary>
        /// 浮遊文字コントローラーを生成します
        /// </summary>
        /// <returns></returns>
        public Common.FloatingText.FloatingTextController Create()
        {
            FloatingText.FloatingTextController controller = GameObject.Instantiate(_floatingTextControllerPrefab, _canvasTransform);
            controller.Initialize(_floatingTextSetting);
            return controller;
        }
    }
}