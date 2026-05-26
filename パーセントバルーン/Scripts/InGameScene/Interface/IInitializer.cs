using UnityEngine;

namespace InGameScene.Interface {
    /// <summary>
    /// MonoBehaviourを継承する初期化処理を行うインターフェース
    /// </summary>
    public interface IInitializer
    {
        /// <summary>
        /// 初期化処理を行う
        /// </summary>
        void Initialize();
    }
}