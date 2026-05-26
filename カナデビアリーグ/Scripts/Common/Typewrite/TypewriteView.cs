using System.Text;
using TMPro;
using UnityEngine;

namespace MinutesGame.Common.Typewrite
{
    public class TypewriteView : MonoBehaviour
    {
        [SerializeField, Header("パソコン画面に表示するテキスト")]
        private TextMeshPro _textMeshPro;

        private const int CLEAR_COUNT = 7;
        private int _currentClearCount = 0;
        
        /// <summary>
        /// テキストをタイプライター風に表示します
        /// </summary>
        public void TypewriteText()
        {
            // 何回"~"を入力するかランダムに決定する
            int rnd = Random.Range(4, 6);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < rnd; i++)
            {
                stringBuilder.Append("-");
            }
            _textMeshPro.text += stringBuilder.ToString();
        }

        /// <summary>
        /// 改行を追加します
        /// </summary>
        public void CreateNewLine()
        {
            _textMeshPro.text += "\n";
            _currentClearCount++;
            if (_currentClearCount >= CLEAR_COUNT)
            {
                ResetText();
                _currentClearCount = 0;
            }
        }

        /// <summary>
        /// 表示内容をリセットします
        /// </summary>
        public void ResetText()
        {
            _textMeshPro.text = "memo:\n";
        }
    }
}