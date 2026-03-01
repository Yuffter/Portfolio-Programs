using Coffee.UIEffects;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Animation
{
    public class GameFinishAnimator : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _gameFinishText;
        [SerializeField]
        private UIEffect _gameFinishEffect;
        private Sequence _seq;

        private void Start()
        {
            _gameFinishText.enabled = false;
        }

        public async UniTask PlayAsync()
        {
            _seq = DOTween.Sequence();
            _seq.AppendCallback(() =>
            {
                _gameFinishText.enabled = true;
            })
            .Append(_gameFinishText.rectTransform.DOShakeAnchorPos(1,20,10,90,false,true))
            .AppendInterval(1f)
            .Append(DOTween.To(() => _gameFinishEffect.transitionRate, x => _gameFinishEffect.transitionRate = x, 1f, 1f).SetEase(Ease.Linear));
            await _seq.AsyncWaitForCompletion();
        }
    }
}