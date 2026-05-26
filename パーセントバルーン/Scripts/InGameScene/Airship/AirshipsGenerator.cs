using System.Collections.Generic;
using UnityEngine;
using InGameScene.Interface;
using TMPro;

namespace InGameScene {
    public class AirshipsGenerator : MonoBehaviour
    {
        [SerializeField, Header("気球のプレファブ")]
        private GameObject _airshipPrefab;

        [SerializeField, Header("バルーンのプレファブ")]
        private List<GameObject> _balloonPrefabs = new List<GameObject>();

        [SerializeField, Header("気球の初期生成位置")]
        private Vector3 _initialAirshipPosition;

        [SerializeField, Header("気球の生成間隔")]
        private float _airshipDistance;

        [SerializeField, Header("バルーンの生成間隔")]
        private float _balloonInterval;

        private List<GameObject> _airships = new List<GameObject>();

        /// <summary>
        /// 気球を班の数分生成する
        /// </summary>
        public List<GameObject> GenerateAirShips() {
            for (int i = 0;i < GameData.Instance.CurrentGroupCount;i++) {
                GameObject airship = Instantiate(
                    _airshipPrefab,
                    _initialAirshipPosition + new Vector3(_airshipDistance, 0, 0) * i,
                    Quaternion.identity);

                airship.GetComponentsInChildren<TextMeshPro>()[1].text = (i+1).ToString();
                _airships.Add(airship);

                GenerateBalloons(airship.transform, _balloonPrefabs[i%_balloonPrefabs.Count]);
            }

            return _airships;
        }

        /// <summary>
        /// 渡された気球にバルーンをつける
        /// </summary>
        /// <param name="airship">気球</param>
        private void GenerateBalloons(Transform airship, GameObject balloonPrefab) {
            Transform insideAirship = airship.Find("InsideAirship").Find("Balloons");
            for (int i = 0;i < GameData.Instance.InitialBalloonCount/_balloonInterval;i++) {
                GameObject balloon = Instantiate(balloonPrefab, insideAirship);
                /* バルーンをランダムな位置に生成して、中心に向かって傾ける */
                balloon.transform.localPosition = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(2f, 3f),
                    0
                );

                Vector3 distance = airship.position - balloon.transform.position;
                float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg + 90;
                balloon.transform.rotation = Quaternion.Euler(0, 0, angle);

                balloon.GetComponent<SpriteRenderer>().sortingOrder = (int)(GameData.Instance.InitialBalloonCount/_balloonInterval) - i;
            }
        }
    }
}
