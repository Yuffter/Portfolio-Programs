using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace InGameScene {
    public class PercentInputGetter
    {
        private Transform _percentInputParent;

        private List<int> _eachPercentValues = new List<int>();

        /// <summary>
        /// 各班のパーセント値
        /// </summary>
        [HideInInspector]
        public IReadOnlyList<int> EachPercentValues {
            get {
                GetPercentInputs();
                return _eachPercentValues;
            }
        }

        /// <summary>
        /// 各班のパーセント値を取得し、リストに格納する
        /// </summary>
        private void GetPercentInputs() {
            _eachPercentValues.Clear();

            /* 入力パネルの取得 */
            _percentInputParent = GameObject.Find("PercentInputs").transform;

            /* 入力パネルの中に含まれてるテキストを全て取得 */
            TMP_InputField[] percentInputs = _percentInputParent.GetComponentsInChildren<TMP_InputField>();
            foreach (var percentInput in percentInputs) {
                /* 半角に変換 */
                percentInput.text = percentInput.text.Normalize(System.Text.NormalizationForm.FormKC);

                /* 入力値に問題がなければ値を格納 */
                try {
                    _eachPercentValues.Add(int.Parse(percentInput.text));
                } catch {
                    _eachPercentValues.Add(0);
                }
            }
        }
    }
}