using Coffee.UIEffects;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using AudioManager.SE;

namespace Title
{
    public class MainAnimator : MonoBehaviour
    {
        [SerializeField] private RectTransform _humanTextRect;
        [SerializeField] private RectTransform _curlingTextRect;
        [SerializeField] private RectTransform _humanRect;
        [SerializeField] private UIEffect _shinyEffect;
        [SerializeField] private TMP_Text _startText;

        public void Initialize()
        {
            _humanTextRect.localScale = Vector3.zero;
            _curlingTextRect.anchoredPosition = new Vector2(-(Screen.width + _curlingTextRect.rect.width), _curlingTextRect.anchoredPosition.y);
            _humanRect.GetComponent<Image>().color = new Color(1,1,1,0);
        }

        public async UniTask PlayOpeningAnimationAsync()
        {
            Sequence seq = DOTween.Sequence();
            await seq
                .AppendInterval(1f)
                .Append(_humanTextRect.DOScale(Vector3.one, 1))
                .Join(_humanTextRect.DORotate(new Vector3(0,0,360 * 10), 1, RotateMode.FastBeyond360).SetEase(Ease.OutCubic))
                .JoinCallback(() => SEManager.Instance.Play(SEName.TitleNingen))
                .AppendCallback(() => SEManager.Instance.Play(SEName.TitleCurling))
                .Append(_curlingTextRect.DOAnchorPosX(400, 1).SetEase(Ease.OutBounce))
                .JoinCallback(() =>
                {
                    DOVirtual.DelayedCall(0.2f,() =>
                    {
                        _curlingTextRect.DORotate(new Vector3(0,0,-15), 0.2f).SetEase(Ease.OutBounce).OnComplete(() =>
                        {
                        _curlingTextRect.DORotate(Vector3.zero, 0.5f).SetEase(Ease.OutBounce);
                        });
                    });
                })
                .AppendCallback(() => SEManager.Instance.Play(SEName.TitleE))
                .Append(_humanRect.GetComponent<Image>().DOColor(new Color(1,1,1,1), 1))
                .Join(_humanRect.DOAnchorPos(new Vector2(_humanRect.anchoredPosition.x, _humanRect.anchoredPosition.y), 1).From(new Vector2(_humanRect.anchoredPosition.x + 50, _humanRect.anchoredPosition.y + 50)).SetEase(Ease.OutCubic))
                .Append(DOTween.To(() => _shinyEffect.transitionRate, x => _shinyEffect.transitionRate = x, 1, 1).SetEase(Ease.Linear))
                .AsyncWaitForCompletion();

            _startText.DOColor(new Color(1,1,1,1), 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }

        public async UniTask PlayTransitionAnimationAsync()
        {
            SEManager.Instance.Play(SEName.TitleStart);
            UIRigidbody humanRigidbody = _humanRect.gameObject.AddComponent<UIRigidbody>();
            humanRigidbody.AddForce(new Vector2(30000, 50000));
            humanRigidbody.AddTorque(10000);
            humanRigidbody.GravityScale = 3f;
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }
    }
}
