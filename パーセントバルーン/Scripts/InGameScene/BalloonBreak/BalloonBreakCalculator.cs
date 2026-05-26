using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace InGameScene {
    public class BalloonBreakCalculator
    {
        private List<float> _groupsDifferencePercent = new List<float>();

        /// <summary>
        /// 各班の差分パーセンテージ
        /// </summary>
        public IReadOnlyList<float> GroupsDifferencePercent => _groupsDifferencePercent;
        public BalloonBreakCalculator() {
            /* 各班の入力値を取得 */
            var percentInputGetter = new PercentInputGetter();
            var groupsPercent = percentInputGetter.EachPercentValues;

            /* 答えのパーセンテージを取得 */
            var correctPercent = GameData.Instance.CurrentQuestion.Percentage;

            /* 各グループの差分を計算 */
            for (int i = 0; i < groupsPercent.Count; i++) {
                int diff = Mathf.Abs(correctPercent - groupsPercent[i]);

                /* もし差分が風船の数を超えている場合 */
                if (diff > GameData.Instance.GroupsData[i].RemainingCount) diff = GameData.Instance.GroupsData[i].RemainingCount;
                _groupsDifferencePercent.Add(diff);

                /* 残りバルーン数を更新しておく */
                GameData.Instance.GroupsData[i].RemainingCount = Mathf.Max(0, GameData.Instance.GroupsData[i].RemainingCount - (int)_groupsDifferencePercent[i]);
            }
        }
    }
}