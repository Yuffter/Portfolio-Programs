using System;
using UniRx;
using UnityEngine;

namespace MinutesGame.Common.Input
{
    public interface IInput
    {
        /// <summary>
        /// 左キーが押されたときのイベント
        /// </summary>
        IObservable<Unit> OnLeftKeyPressed { get; }
        /// <summary>
        /// 右キーが押されたときのイベント
        /// </summary>
        IObservable<Unit> OnRightKeyPressed { get; }
        /// <summary>
        /// 上キーが押されたときのイベント
        /// </summary>
        IObservable<Unit> OnUpKeyPressed { get; }
        /// <summary>
        /// 下キーが押されたときのイベント
        /// </summary>
        IObservable<Unit> OnDownKeyPressed { get; }

        /// <summary>
        /// 右キーが押され続けているかどうか
        /// </summary>
        bool IsHoldingRight { get; }

        /// <summary>
        /// 入力を有効化します
        /// </summary>
        void EnableInput();

        /// <summary>
        /// 入力を無効化します
        /// </summary>
        void DisableInput();
    }
}