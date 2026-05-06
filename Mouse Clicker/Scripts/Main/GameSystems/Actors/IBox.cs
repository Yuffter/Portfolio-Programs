using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Main.GameSystems.Actors
{
    public interface IBox
    {
        /// <summary>
        /// マウスを生成する
        /// </summary>
        UniTask SpawnMouseAsync();
        /// <summary>
        /// マウスを破棄する
        /// </summary>
        UniTask DespawnMouseAsync();
        /// <summary>
        /// たんすを引き出す
        /// </summary>
        /// <returns></returns>
        UniTask PullOutAsync();

        /// <summary>
        /// ボックスの現在のステート
        /// </summary>
        public BoxType BoxType { get; }
    }

    public enum BoxType
    {
        Normal,
        MouseSpawning,
        MouseDespawn,
        PullOut
    }
}