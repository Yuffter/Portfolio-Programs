using Project.Main.GameSystems.Presentation;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using KanKikuchi.AudioManager;

namespace Project.Main.HUD
{
    public class GameFinishView : MonoBehaviour, IFinishPresentation
    {
        [SerializeField] private GameObject _finishText;
        private CancellationToken _cancellationToken;

        private void OnEnable()
        {
            _cancellationToken = this.GetCancellationTokenOnDestroy();
            _finishText.SetActive(false);
        }

        public async UniTask ShowFinishTextAsync()
        {
            _finishText.SetActive(true);
            SEManager.Instance.Play(SEPath.FINISH_GAME);
            await _finishText.transform.DOShakePosition(1f, 50).WithCancellation(_cancellationToken);
        }
    }
}
