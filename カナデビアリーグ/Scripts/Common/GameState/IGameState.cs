using UnityEngine;

namespace MinutesGame.Common.GameState
{
    public interface IGameState
    {
        /// <summary>
        /// このステートに入った際に呼ばれます
        /// </summary>
        void Enter();
        /// <summary>
        /// このステートの際に毎フレーム呼ばれます
        /// </summary>
        void Update();
        /// <summary>
        /// このステートから出る際に呼ばれます
        /// </summary>
        void Exit();
    }
}