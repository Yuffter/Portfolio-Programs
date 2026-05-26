using UnityEngine;

namespace InGameScene {
    public class AirshipEffectFactory
    {
        private GameObject _explosionEffect;
        private GameObject _fallingEffect;

        public AirshipEffectFactory(GameObject explosionEffect, GameObject fallingEffect) {
            _explosionEffect = explosionEffect;
            _fallingEffect = fallingEffect;
        }

        /// <summary>
        /// 爆発エフェクトを生成する
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public GameObject CreateExplosionEffect(Vector3 position) {
            return GameObject.Instantiate(_explosionEffect, position, Quaternion.identity);
        }

        /// <summary>
        /// 落下エフェクトを生成する
        /// </summary>
        /// <param name="position"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public GameObject CreateFallingEffect(Vector3 position, Transform parent = null) {
            var effect = GameObject.Instantiate(_fallingEffect, position, Quaternion.identity);

            if (parent != null)
                effect.transform.SetParent(parent);

            return  effect;
        }
    }
}