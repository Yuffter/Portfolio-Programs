using Coffee.UIEffects;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using AudioManager.SE;

namespace Animation {
    public class GameStartAnimator : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _upBorder;
        [SerializeField]
        private UIEffect _upBorderEffect;
        [SerializeField]
        private RectTransform _downBorder;
        [SerializeField]
        private UIEffect _downBorderEffect;
        [SerializeField]
        private TMP_Text _gameStartText;
        Sequence _seq;

        public async UniTask PlayAsync()
        {
            _seq = DOTween.Sequence();
            _seq.Append(DOTween.To(() => _upBorderEffect.transitionRate, x => _upBorderEffect.transitionRate = x, 0f, 1f));
            _seq.Join(DOTween.To(() => _downBorderEffect.transitionRate, x => _downBorderEffect.transitionRate = x, 0f, 1f));
            _seq.Append(_gameStartText.rectTransform.DOAnchorPosX(0,1f).From(new Vector2(-1200,0)).SetEase(Ease.OutExpo));
            _seq.JoinCallback(() => SEManager.Instance.Play(SEName.StartGame));
            _seq.AppendInterval(1f);
            _seq.AppendCallback(() =>
            {
                _gameStartText.SetText("スタート！");
                _gameStartText.characterSpacing = -100;
                _gameStartText.alpha = 1;
            });
            _seq.Append(DOTween.To(() => _gameStartText.characterSpacing, x => _gameStartText.characterSpacing = x, 50, 2f).SetEase(Ease.OutExpo));
            _seq.JoinCallback(() => SEManager.Instance.Play(SEName.GameStart));
            _seq.Join(_gameStartText.DOFade(0,1.5f).SetDelay(0.5f));
            _seq.Append(DOTween.To(() => _upBorderEffect.transitionRate, x => _upBorderEffect.transitionRate = x, 1f, 1f));
            _seq.Join(DOTween.To(() => _downBorderEffect.transitionRate, x => _downBorderEffect.transitionRate = x, 1f, 1f));
            await _seq.AsyncWaitForCompletion();
        }
    }
}
