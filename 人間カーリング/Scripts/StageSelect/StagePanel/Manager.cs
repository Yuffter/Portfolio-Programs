using System.Collections.Generic;
using UnityEngine;

namespace StageSelect.StagePanel
{
    /// <summary>
    /// ステージパネル群を操作するためのファサードクラス
    /// </summary>
    /// <remarks>
    /// StageSelectStateでインスタンス化して使用する <br/>
    /// 必ずSetStagePanelsメソッドを呼び出し、ステージパネルリストを設定すること
    /// </remarks>
    public class Manager : MonoBehaviour
    {
        private List<Controller> _stagePanelControllers;
        private IndexCalculator _indexCalculator;

        public void Initialize(List<Controller> stagePanelControllers, IndexCalculator indexCalculator)
        {
            _stagePanelControllers = stagePanelControllers;
            _indexCalculator = indexCalculator;
            FocusCurrentPanel();
        }

        /// <summary>
        /// 左に移動する
        /// </summary>
        public void MoveLeft()
        {
            UnfocusCurrentPanel();
            if (_indexCalculator.MoveLeft() == false)
            {
                // 変化なし
                FocusCurrentPanel();
                return;
            }
            FocusCurrentPanel();

            foreach (var panel in _stagePanelControllers)
            {
                panel.MoveRight();
            }
        }

        /// <summary>
        /// 右に移動する
        /// </summary>
        public void MoveRight()
        {
            UnfocusCurrentPanel();
            if (_indexCalculator.MoveRight() == false)
            {
                // 変化なし
                FocusCurrentPanel();
                return;
            }
            FocusCurrentPanel();

            foreach (var panel in _stagePanelControllers)
            {
                panel.MoveLeft();
            }
        }

        private void FocusCurrentPanel()
        {
            _stagePanelControllers[_indexCalculator.CurrentIndex.CurrentValue].Focus();
        }

        private void UnfocusCurrentPanel()
        {
            _stagePanelControllers[_indexCalculator.CurrentIndex.CurrentValue].Unfocus();
        }
    }
}
