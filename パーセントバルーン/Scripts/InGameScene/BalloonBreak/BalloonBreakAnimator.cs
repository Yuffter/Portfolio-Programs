using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using InGameScene.Interface;
using KanKikuchi.AudioManager;
using UnityEngine;

namespace InGameScene {
    public class BalloonBreakAnimator : MonoBehaviour
    {
        [SerializeField, Header("気球提供クラス")]
        private AirshipsProvider _airshipsProvider;

        [SerializeField, Header("破裂エフェクト")]
        private GameObject _effectPrefab;
        /// <summary>
        /// 各班のバルーンを破壊する
        /// </summary>
        /// <param name="groupsDifferencePercent"></param>
        /// <returns></returns>
        public async UniTask BreakEachGroupBalloons(IReadOnlyList<float> groupsDifferencePercent) {
            await UniTask.Delay(TimeSpan.FromSeconds(1));

            for (int i = 1;i <= groupsDifferencePercent.Max();i++) {
                for (int j = 0;j < _airshipsProvider.Airships.Count;j++) {
                    if (groupsDifferencePercent[j] >= i) {
                        /* エフェクトの座標を設定 */
                        Vector3 effectPosition = _airshipsProvider.EachAirshipsBalloonList[j][0].transform.position;
                        effectPosition += _airshipsProvider.EachAirshipsBalloonList[j][0].transform.up.normalized * 1f;

                        /* エフェクトを生成し、バルーンを破壊 */
                        GameObject effect = Instantiate(_effectPrefab, effectPosition, Quaternion.identity);
                        Destroy(_airshipsProvider.EachAirshipsBalloonList[j][0]);
                        _airshipsProvider.EachAirshipsBalloonList[j].RemoveAt(0);

                        _airshipsProvider.RemainingTextList[j].text = (int.Parse(_airshipsProvider.RemainingTextList[j].text)-1).ToString();

                        SEManager.Instance.Play(SEPath.CRACK_BALLOON);
                    }
                }

                await UniTask.Delay(TimeSpan.FromSeconds(0.05f));
            }
        }

        /// <summary>
        /// 風船の数を表示する
        /// </summary>
        public void SetTexts() {
            for (int i = 0;i < GameData.Instance.CurrentGroupCount;i++) {
                _airshipsProvider.RemainingTextList[i].text = GameData.Instance.InitialBalloonCount.ToString();
            }
        }

        private void Start()
        {
            SetTexts();
        }
    }
}
