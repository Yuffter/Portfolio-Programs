using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace MinutesGame.Common.DialogBox
{
    public class DialogBoxController : MonoBehaviour
    {
        private RectTransform _rectTransform;
        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.localScale = new Vector3(0f, 0f, 1f);
        }

        public async UniTask OpenDialogBoxAsync()
        {
            await _rectTransform.DOScale(1f, 0.3f).From(new Vector3(0f, 0f, 1f)).SetEase(Ease.OutBounce).AsyncWaitForCompletion();
        }
    }
}