using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using KanKikuchi.AudioManager;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace InGameScene {
    public class AirshipsBreaker
    {
        private AirshipsProvider _airshipsProvider;
        private AirshipEffectFactory _airshipEffectFactory;
        private Volume _volume;
        private ChromaticAberration _chromaticAberration;
        private FilmGrain _filmGrain;
        public AirshipsBreaker(AirshipsProvider airshipsProvider, AirshipEffectFactory airshipEffectFactory) {
            _airshipsProvider = airshipsProvider;
            _airshipEffectFactory = airshipEffectFactory;

            _volume = GameObject.FindObjectOfType<Volume>();
            _volume.profile.TryGet(out _chromaticAberration);
            _volume.profile.TryGet(out _filmGrain);
        }

        public async UniTask Break() {
            /* 残りのバルーンの数が0の班があるか調べる */
            List<GameObject> breakingAirships = new();
            for (int i = 0;i < GameData.Instance.CurrentGroupCount;i++) {
                if (GameData.Instance.GroupsData[i].RemainingCount == 0 && _airshipsProvider.Airships[i] != null) {
                    breakingAirships.Add(_airshipsProvider.Airships[i]);
                }
            }

            if (breakingAirships.Count == 0) {
                return;
            }

            /* 落下開始 */
            for (int i = 0;i < breakingAirships.Count;i++) {
                GameObject airship = breakingAirships[i];
                airship.AddComponent<Rigidbody2D>();
                SEManager.Instance.Play(SEPath.FALL);
                _airshipEffectFactory.CreateFallingEffect(airship.transform.position, airship.transform);
                airship.AddComponent<AirshipFallObserver>();
            }


            var tasks = breakingAirships.Select(airship => airship.GetComponent<AirshipFallObserver>().OnAirshipFall.First().ToUniTask()).ToArray();
            /* どれか1つの気体が地面に着くまで待機 */
            await UniTask.WhenAny(tasks);

            /* 爆発エフェクトを再生 */
            _chromaticAberration.intensity.value = 1;
            _filmGrain.intensity.value = 1;
            foreach (var airship in breakingAirships) {
                _airshipEffectFactory.CreateExplosionEffect(airship.transform.position);
                SEManager.Instance.Play(SEPath.EXPLOSION);
                GameObject.Destroy(airship);
            }

            DOTween.To(() => _chromaticAberration.intensity.value, x => _chromaticAberration.intensity.value = x, 0, 1).SetLink(_volume.gameObject);
            DOTween.To(() => _filmGrain.intensity.value, x => _filmGrain.intensity.value = x, 0, 1).SetLink(_volume.gameObject);
        }
    }

}