using Cysharp.Threading.Tasks;
using Project.Main.GameSystems.Presentation;
using TMPro;
using UnityEngine;
using DG.Tweening;
using KanKikuchi.AudioManager;

namespace Project.Main.HUD
{
    public class CountDownView : MonoBehaviour, ICountDownPresentation
    {
        [SerializeField] private TMP_Text _countDownText;
        private Sequence _seq;

        public async UniTask StartCountDownAsync(float duration)
        {
            float remainingTime = duration;
            SEManager.Instance.Play(SEPath.COUNT_DOWN);
            while (remainingTime > 0)
            {
                _seq?.Kill();
                _seq = DOTween.Sequence();
                _seq.AppendCallback(() =>
                {
                    _countDownText.SetText(remainingTime.ToString());
                    _countDownText.color = new Color(1, 0.25f, 0, 1);
                    _countDownText.rectTransform.localScale = Vector3.one;
                })
                .Append(_countDownText.rectTransform.DOScale(new Vector3(1.5f, 1.5f), 1f))
                .Join(_countDownText.DOFade(0, 1.2f));
                await UniTask.Delay(1000);
                remainingTime--;
            }

            _seq?.Kill();
            _seq = DOTween.Sequence();
            await _seq.AppendCallback(() =>
            {
                _countDownText.color = new Color(1, 0.25f, 0, 1);
                _countDownText.rectTransform.localScale = new Vector3(1.1f, 1.1f);
                _countDownText.SetText("<size=250>START!</size>");
            })
            .Append(_countDownText.rectTransform.DOScale(new Vector3(1f, 1f), 0.5f).SetEase(Ease.OutBounce))
            .Join(_countDownText.DOFade(0, 2f)).AsyncWaitForCompletion();
        }
    }
}