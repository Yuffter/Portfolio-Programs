using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Main.GameSystems.Presentation;
using UnityEngine;

namespace Project.Main.HUD
{
    public class TransitionView : MonoBehaviour, ITransitionPresentation
    {
        private CancellationToken _cancellationToken;
        [SerializeField] private TransitionProgressController _transitionProgressController;

        private void OnEnable()
        {
            _cancellationToken = this.GetCancellationTokenOnDestroy();
        }

        public async UniTask FadeInAsync(float duration)
        {
            _transitionProgressController.progress = 0f;
            await DOTween.To(() => _transitionProgressController.progress, x => _transitionProgressController.progress = x, 1f, duration)
                .SetEase(Ease.Linear).WithCancellation(_cancellationToken);
        }

        public async UniTask FadeOutAsync(float duration)
        {
            _transitionProgressController.progress = 1f;
            await DOTween.To(() => _transitionProgressController.progress, x => _transitionProgressController.progress = x, 0f, duration)
                .SetEase(Ease.Linear).WithCancellation(_cancellationToken);
        }
    }
}