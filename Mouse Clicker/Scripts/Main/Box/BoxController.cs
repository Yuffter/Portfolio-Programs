using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using KanKikuchi.AudioManager;
using Project.Main.GameSystems.Actors;
using Project.Main.GameSystems.ScriptableObjects;
using UnityEngine;

namespace Project.Main.Box
{
    public class BoxController : MonoBehaviour, IBox
    {
        private Transform _mouseTransform;
        private Transform _transform;
        private SpriteRenderer _spriteRenderer;
        private SpriteRenderer _mouseSpriteRenderer;
        private Vector3 _initialPosition;
        private BoxType _boxType = BoxType.Normal;
        public BoxType BoxType => _boxType;
        private Sequence _sequence;
        private CancellationToken _cancellationToken;
        private GameRule _gameRule;
        public async UniTask DespawnMouseAsync()
        {
            _boxType = BoxType.MouseDespawn;
            _sequence = DOTween.Sequence();
            await _sequence.Append(_mouseTransform.DOLocalMoveY(0f, 0.2f))
                .Join(transform.DOMoveY(0.05f, 0.2f).SetRelative())
                .AppendCallback(() => _boxType = BoxType.Normal)
                .WithCancellation(_cancellationToken);
        }

        public async UniTask SpawnMouseAsync()
        {
            _boxType = BoxType.MouseSpawning;
            SEManager.Instance.Play(SEPath.MOUSE_SPAWN);
            _sequence = DOTween.Sequence();
            await _sequence.Append(_mouseTransform.DOLocalMoveY(0.1f, 0.1f))
                .Join(transform.DOMoveY(-0.05f, 0.1f).SetRelative())
                .AppendInterval(_gameRule.MouseDespawnTime)
                .AppendCallback(() => DespawnMouseAsync().Forget())
                .WithCancellation(_cancellationToken);
        }

        public async UniTask PullOutAsync()
        {
            _boxType = BoxType.PullOut;
            _sequence.Kill();
            _sequence = DOTween.Sequence();
            await _sequence.Append(transform.DOScale(new Vector3(3, 3), 0.5f).SetRelative())
            .Join(_spriteRenderer.DOFade(0, 0.3f))
            .Join(_mouseSpriteRenderer.DOFade(0, 0.3f))

            .AppendInterval(0.1f)

            .AppendCallback(() =>
            {
                _mouseTransform.localPosition = Vector3.zero;
                _transform.localPosition = _initialPosition;
                _mouseSpriteRenderer.DOFade(0, 0);
            })
            .Append(transform.DOScale(new Vector3(-3, -3), 0.5f).SetRelative())
            .Join(_spriteRenderer.DOFade(1, 0.3f))
            .AppendCallback(() => _mouseSpriteRenderer.DOFade(1, 0f))
            .AppendCallback(() => _boxType = BoxType.Normal)
            .WithCancellation(_cancellationToken);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="gameRule"></param>
        public void Initialize(GameRule gameRule)
        {
            _gameRule = gameRule;

            _mouseTransform = transform.Find("Mouse");
            _transform = transform;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _mouseSpriteRenderer = _mouseTransform.GetComponent<SpriteRenderer>();
            _cancellationToken = this.GetCancellationTokenOnDestroy();
            _initialPosition = _transform.localPosition;
        }
    }
}