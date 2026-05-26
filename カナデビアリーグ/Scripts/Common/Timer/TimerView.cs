using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MinutesGame.Common.Timer
{
    public class TimerView : MonoBehaviour, ITimerView
    {
        [SerializeField]
        private TMP_Text _timerText;
        [SerializeField]
        private Image _timerFillImage;
        [SerializeField]
        private Gradient _fillGradient;
        [SerializeField]
        private RectTransform _timerRectTransform;
        private float _maxTime;
        private bool _isAlarted = false;

        public void Initialize(float maxTime)
        {
            _maxTime = maxTime;
        }
        
        /// <summary>
        /// タイマーのテキストを設定します
        /// </summary>
        /// <param name="text">表示するテキスト</param>
        public void SetTimerText(float text)
        {
            _timerText.text = ((int)text).ToString();
            _timerFillImage.fillAmount = text / _maxTime;
            _timerFillImage.color = _fillGradient.Evaluate(1 - (_timerFillImage.fillAmount));

            if ((int)text <= 10 && !_isAlarted)
            {
                _timerRectTransform.DOShakeAnchorPos(0.5f,20f,10,90,false,true,ShakeRandomnessMode.Harmonic);
                _timerRectTransform.DOScale(2,0.5f).SetEase(Ease.OutElastic);
                _isAlarted = true;
            }
        }
    }
}