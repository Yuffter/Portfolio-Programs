using UnityEngine;

namespace Project.Main.GameSystems.Actors
{
    public interface ICursor
    {
        /// <summary>
        /// 初期化処理を行う
        /// </summary>
        /// <param name="position"></param>
        void Initialize(Vector2Int position);
        /// <summary>
        /// 現在のカーソル位置を取得する
        /// </summary>
        /// <value></value>
        Vector2Int CurrentPosition { get; }

        /// <summary>
        /// カーソルの操作を停止する
        /// </summary>
        void Stop();
    }
}
