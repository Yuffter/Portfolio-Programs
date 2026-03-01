using UnityEngine;

namespace Common
{
    public abstract class StateBase
    {
        
        /// <summary>
        /// このステートに入ったときに呼ばれる
        /// </summary>
        public virtual void Enter()
        {
        }

        /// <summary>
        /// このステートが実行されている間呼ばれる
        /// </summary>
        public virtual void Update()
        {
        }

        /// <summary>
        /// このステートから出るときに呼ばれる
        /// </summary>
        public virtual void Exit()
        {
        }
    }
}