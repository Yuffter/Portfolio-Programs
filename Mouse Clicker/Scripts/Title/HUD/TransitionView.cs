using UnityEngine;
using Cysharp.Threading.Tasks;
using Project.Title.GameSystems.Presentation;
using DG.Tweening;

namespace Project.Title.HUD
{
    public class TransitionView : MonoBehaviour, ITransitionPresentation
    {
        [SerializeField] private TransitionProgressController _transitionProgressController;

        public async UniTask StartTransitionAsync()
        {
            _transitionProgressController.progress = 0f;
            await DOTween.To(
                () => _transitionProgressController.progress,
                x => _transitionProgressController.progress = x,
                1f,
                1f)
                .SetEase(Ease.Linear).AsyncWaitForCompletion();
        }
    }
}