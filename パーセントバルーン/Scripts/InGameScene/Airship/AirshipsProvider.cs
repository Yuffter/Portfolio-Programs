using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

namespace InGameScene {
    public class AirshipsProvider : MonoBehaviour
    {
        private List<GameObject> _airships = new();
        /// <summary>
        /// 生成された気球のリスト
        /// </summary>
        public IReadOnlyList<GameObject> Airships => _airships;

        /// <summary>
        /// 各班の気球のバルーンリスト
        /// </summary>
        public List<List<GameObject>> EachAirshipsBalloonList = new();

        public List<TextMeshPro> RemainingTextList = new();

        /// <summary>
        /// 気球を設定する
        /// </summary>
        /// <param name="airships"></param>
        internal void SetAirships(List<GameObject> airships) {
            _airships = airships;

            /* 各班のバルーンをリストに格納 */
            for (int i = 0;i < airships.Count;i++) {
                EachAirshipsBalloonList.Add(new List<GameObject>());
                foreach (Transform balloon in airships[i].transform.Find("InsideAirship").Find("Balloons")) {
                    EachAirshipsBalloonList[i].Add(balloon.gameObject);
                }

                RemainingTextList.Add(airships[i].transform.Find("InsideAirship").GetComponentInChildren<TextMeshPro>());
            }
        }
    }
}