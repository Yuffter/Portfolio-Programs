using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class HoverEffect : MonoBehaviour
    {
        private Renderer _renderer;
        private bool _isEnableEffect = false;

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
        }

        /// <summary>
        /// ホバーエフェクトを無効化する
        /// </summary>
        public void EnableEffect()
        {
            _isEnableEffect = true;
        }

        /// <summary>
        /// ホバーエフェクトを有効化する
        /// </summary>
        public void DisableEffect()
        {
            _isEnableEffect = false;
        }

        /// <summary>
        /// マウスがオブジェクトにホバーしたときの処理
        /// </summary>
        private void OnMouseEnter()
        {
            if (_isEnableEffect == false) return;

            // この子の全てのRendererのRenderingLayerMaskのLight Layer 1を追加
            foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
            {
                childRenderer.renderingLayerMask = (1u << 0) | (1u << 1);
            }
        }

        /// <summary>
        /// マウスがオブジェクトから離れたときの処理
        /// </summary>
        private void OnMouseExit()
        {
            // Light Layer 1を削除
            foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
            {
                childRenderer.renderingLayerMask = 1u << 0;
            }
        }
    }
}