using System.Collections.Generic;
using UnityEngine;

namespace GeneralResultScene {
    /// <summary>
    /// ポイント計算クラス
    /// </summary>
    public class PointCalculator
    {
        /// <summary>
        /// 総合得点を計算する
        /// </summary>
        /// <returns></returns>
        public List<GroupPointData> CalculatePoint()
        {
            // ポイント計算処理
            List<GroupPointData> groupPointDataList = new();
            int index = 0;
            foreach (var groupData in GameData.Instance.GroupsData)
            {
                int point = 0;
                index++;

                point += groupData.RemainingCount * GameData.Instance.PercentBalloonPointMultiplier;
                point += groupData.SearchRallyPoint * GameData.Instance.SearchRallyPointMultiplier;

                GroupPointData groupPointData = new GroupPointData
                {
                    GroupNumber = index,
                    Point = point
                };
                groupPointDataList.Add(groupPointData);
            }

            /* ポイントの高い順にソート */
            groupPointDataList.Sort((a, b) => b.Point - a.Point);
            return groupPointDataList;
        }
    }

    public class GroupPointData {
        public int GroupNumber;
        public int Point;
    }
}