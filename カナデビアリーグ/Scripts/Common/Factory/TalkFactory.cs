using UnityEngine;
using VContainer;

namespace MinutesGame.Common.Factory
{
    public class TalkFactory : Common.Interfaces.IFactory<GameObject>
    {
        [Inject]
        private GameObject _talkPrefab;

        /// <summary>
        /// Talkオブジェクトを生成します
        /// </summary>
        /// <returns></returns>
        public GameObject Create()
        {
            return GameObject.Instantiate(_talkPrefab);
        }
    }
}