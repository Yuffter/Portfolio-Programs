using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Main.GameSystems.Actors;
using UnityEngine;

namespace Project.Main.Camera
{
    public class CameraController : MonoBehaviour, ICamera
    {
        [SerializeField] private UnityEngine.Camera _camera;
        private Sequence _seq;
        private CancellationToken _cancellationToken;

        private void Awake()
        {
            _cancellationToken = this.GetCancellationTokenOnDestroy();
        }
        public void PlayPullOutAnimation()
        {
            _camera.transform.DOKill(true);
            _seq = DOTween.Sequence();
            _seq.Append(_camera.DOShakePosition(0.1f, 0.5f, 5));
            _seq.Join(_camera.DOOrthoSize(5, 1f).SetEase(Ease.OutElastic).From(4.7f)).WithCancellation(_cancellationToken);
        }
    }
}