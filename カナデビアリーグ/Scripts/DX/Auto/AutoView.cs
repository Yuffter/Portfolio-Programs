using UnityEngine;

namespace MinutesGame.DX.Auto
{
    public class AutoView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        /// <summary>
        /// 点滅を開始する
        /// </summary>
        public void StartFlashing()
        {
            _animator.SetBool("IsFlashing", true);
        }

        /// <summary>
        /// 点滅を停止する
        /// </summary>
        public void StopFlashing()
        {
            _animator.SetBool("IsFlashing", false);
        }
    }
}
