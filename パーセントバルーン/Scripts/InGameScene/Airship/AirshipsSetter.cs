using System.Collections.Generic;
using UnityEngine;

namespace InGameScene {
    public class AirshipsSetter
    {
        private readonly AirshipsProvider _airshipsProvider = new AirshipsProvider();
        
        public AirshipsSetter(AirshipsProvider airshipsProvider) {
            _airshipsProvider = airshipsProvider;
        }

        /// <summary>
        /// 気球を設定する
        /// </summary>
        /// <param name="airships"></param>
        public void SetAirships(List<GameObject> airships) {
            _airshipsProvider.SetAirships(airships);
        }
    }
}