using UnityEngine;

namespace InGameScene.Interface {
    /// <summary>
    /// 自前インスタンス化を利用して初期化処理を行うインターフェース
    /// </summary>
    public interface IManualInitializer
    {
        /// <summary>
        /// 初期化処理を行う
        /// </summary>
        void Initialize();
    }
}