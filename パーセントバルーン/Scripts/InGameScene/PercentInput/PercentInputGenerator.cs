using UnityEngine;
using InGameScene.Interface;
using TMPro;
using System.Collections.Generic;

namespace InGameScene {
    public class PercentInputGenerator : MonoBehaviour, IInitializer
    {
        [SerializeField, Header("パーセント入力のプレファブ")]
        private GameObject _percentInputPrefab;

        [SerializeField, Header("パーセント入力を配置する親オブジェクト")]
        private Transform _percentInputParent;

        private List<string> _circleNumbers = new List<string> { "①", "②", "③", "④", "⑤", "⑥", "⑦", "⑧", "⑨", "⑩" };
        
        public void Initialize()
        {
            GeneratePercentInput();
        }

        /// <summary>
        /// 班の数分パーセント入力欄を生成する
        /// </summary>
        private void GeneratePercentInput() {
            for (int i = 0;i < GameData.Instance.CurrentGroupCount;i++) {
                GameObject percentInput = Instantiate(_percentInputPrefab, _percentInputParent);
                TMP_Text groupNumberLabel = percentInput.transform.Find("GroupNumberLabel").GetComponent<TMP_Text>();
                groupNumberLabel.SetText(_circleNumbers[i]);
            }
        }
    }
}