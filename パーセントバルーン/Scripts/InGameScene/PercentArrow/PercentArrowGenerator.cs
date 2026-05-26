using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace InGameScene {
    public class PercentArrowGenerator : MonoBehaviour
    {
        [SerializeField, Header("矢印のプレファブ")]
        private GameObject _percentArrowPrefab;

        [SerializeField, Header("パーセントゲージのRectTransform")]
        private RectTransform _percentGaugeRectTransform;

        [SerializeField, Header("キャンバス")]
        private Transform _canvas;

        private List<Color> _colors = new List<Color> {
            new Color(1.0f, 0.0f, 0.0f, 1.0f), /* 赤 */
            new Color(0.5f, 0.0f, 1.0f, 1.0f), /* 紫 */
            new Color(1.0f, 0.0f, 1.0f, 1.0f), /* ピンク */
            new Color(1.0f, 1.0f, 0.0f, 1.0f), /* 黄 */
            new Color(0.0f, 1.0f, 0.0f, 1.0f), /* 緑 */
            new Color(0.0f, 0.0f, 1.0f, 1.0f), /* 青 */
        };

        private List<GameObject> _arrows = new List<GameObject>();

        /// <summary>
        /// 各班のパーセントをもとに矢印を生成する
        /// </summary>
        public void Generate() {
            /* 各班のパーセントを取得 */
            PercentInputGetter percentInputGetter = new PercentInputGetter();
            var eachPercentValues = percentInputGetter.EachPercentValues;

            /* パーセントゲージのRectTransformを基に左端のX座標を計算 */
            float gaugeWidth = _percentGaugeRectTransform.rect.width;
            float gaugeLeftCornerX = _percentGaugeRectTransform.anchoredPosition.x - gaugeWidth / 2;

            float gaugeHeight = _percentGaugeRectTransform.rect.height;
            float gaugeTopCornerY = _percentGaugeRectTransform.anchoredPosition.y + gaugeHeight;
            Logger.LoggerManager.Log($"gaugeTopCornerY: {gaugeTopCornerY}");

            _arrows.Clear();

            /* 矢印を生成 */
            for (int i = 0; i < eachPercentValues.Count; i++) {
                /* 矢印の生成位置を計算 */
                float arrowX = gaugeLeftCornerX + gaugeWidth * (eachPercentValues[i] / 100f);
                Vector3 arrowPosition = new Vector3(arrowX, gaugeTopCornerY + gaugeHeight / 2);

                /* 矢印の生成 */
                GameObject percentArrow = Instantiate(_percentArrowPrefab, _canvas);
                _arrows.Add(percentArrow);
                percentArrow.GetComponent<RectTransform>().anchoredPosition = arrowPosition;
                percentArrow.GetComponentInChildren<Image>().color = _colors[i % _colors.Count];
            }
        }

        /// <summary>
        /// 矢印削除アニメーションを再生する
        /// </summary>
        /// <returns></returns>
        public async UniTask DeleteArrows() {
            for (int i = 0;i < _arrows.Count;i++) {
                _arrows[i].GetComponent<RectTransform>().DOAnchorPosY(-200, 1).SetEase(Ease.InBack).SetLink(_arrows[i]);
                await UniTask.Delay(TimeSpan.FromSeconds(0.05f));
            }

            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
            foreach (var g in _arrows) {
                Destroy(g);
            }
        }
    }
}
