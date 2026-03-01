using R3;
using Unity.VisualScripting;
using UnityEngine;

namespace StageSelect.StagePanel
{
    /// <summary>
    /// 指定された範囲内で現在のインデックスを追跡および更新する機能を提供し、境界内での左右への移動をサポートします。
    /// </summary>
    /// <remarks>インデックスは構築時に指定された最小値で初期化され、最小インデックスと最大インデックスで定義された
    /// 包括的な範囲内に制約されます。このクラスは、コレクションやメニューを移動する際に、現在の位置が固定された
    /// 境界内に留まる必要があるシナリオで役立ちます。</remarks>
    public class IndexCalculator
    {
        private ReactiveProperty<int> _currentIndex = new ReactiveProperty<int>(0);
         public ReadOnlyReactiveProperty<int> CurrentIndex => _currentIndex;

        private int _minIndex;
        private int _maxIndex;

        public int MinIndex => _minIndex;
        public int MaxIndex => _maxIndex;

        public void Initialize(int minIndex, int maxIndex)
        {
            _minIndex = minIndex;
            _maxIndex = maxIndex;
            _currentIndex = new ReactiveProperty<int>(minIndex);
        }

        /// <summary>
        /// フォーカスを左に移動する
        /// </summary>
        /// <returns></returns>
        public bool MoveLeft()
        {
            int previousIndex = _currentIndex.Value;
            _currentIndex.Value = Mathf.Clamp(_currentIndex.Value - 1, _minIndex, _maxIndex);
            return previousIndex != _currentIndex.Value;
        }

        /// <summary>
        /// フォーカスを右に移動する
        /// </summary>
        /// <returns></returns>
        public bool MoveRight()
        {
            int previousIndex = _currentIndex.Value;
            _currentIndex.Value = Mathf.Clamp(_currentIndex.Value + 1, _minIndex, _maxIndex);
            return previousIndex != _currentIndex.Value;
        }
    }
}
