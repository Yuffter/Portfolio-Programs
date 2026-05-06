using UnityEngine;
using Project.Title.GameSystems.Presentation;
using TMPro;
using Cysharp.Threading.Tasks;
using R3;
using DG.Tweening;
using System.Threading;

namespace Project.Title.HUD
{
    public class FlashTextView : MonoBehaviour, IFlashTextPresentation
    {
        [SerializeField] private TextMeshProUGUI _text;
        private Sequence _flashSequence;

        public void StartAnimation()
        {
            Debug.Log("Flash animation started.");
            _flashSequence = DOTween.Sequence()
                .Append(DOTween.To(() => _text.alpha, x => _text.alpha = x, 0, 1).SetEase(Ease.InOutSine))
                .Append(DOTween.To(() => _text.alpha, x => _text.alpha = x, 1, 1).SetEase(Ease.InOutSine))
                .SetLoops(-1);
        }

        public void StopAnimation()
        {
            Debug.Log("Flash animation stopped.");
            _flashSequence?.Kill();
        }
    }
}