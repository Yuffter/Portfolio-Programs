using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "GuideTextDatabase", menuName = "ScriptableObject/Create GuideTextDatabase")]
    public class GuideTextDatabase : ScriptableObject
    {
        [SerializeField, Header("ガイドテキスト"),Multiline(3)]
        private string _overViewText = "操作するキャラクターを選択しよう！\n(WASDで移動可能)";
        public string OverViewText => _overViewText;

        [SerializeField, Multiline(3)]
        private string _focusText = "キャラクターを選択すると\nカメラが追従します";
        public string FocusText => _focusText;
    }
}