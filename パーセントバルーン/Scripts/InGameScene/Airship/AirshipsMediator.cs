using UnityEngine;
using InGameScene.Interface;

namespace InGameScene {
    public class AirshipsMediator : MonoBehaviour, IInitializer
    {
        [SerializeField, Header("気球生成クラス")]
        private AirshipsGenerator _airshipsGenerator;

        [SerializeField, Header("気球提供クラス")]
        private AirshipsProvider _airshipsProvider;

        public void Initialize()
        {
            /* 気球を生成し、提供クラスに渡す */
            AirshipsSetter airshipsSetter = new AirshipsSetter(_airshipsProvider);
            airshipsSetter.SetAirships(_airshipsGenerator.GenerateAirShips());
        }
    }
}