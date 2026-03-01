using System;
using System.Collections.Generic;
using UnityEngine;

namespace State
{
    /// <summary>
    /// ステートのインターフェース
    /// </summary>
    public interface IState : IDisposable
    {
        /// <summary>
        /// このステートに変更されたときに呼ばれる
        /// </summary>
        void OnEnter();
        /// <summary>
        /// このステートが終了するときに呼ばれる
        /// </summary>
        void OnExit();
        /// <summary>
        /// このステートの時に毎フレーム呼ばれる
        /// </summary>
        void OnUpdate();
    }
}
