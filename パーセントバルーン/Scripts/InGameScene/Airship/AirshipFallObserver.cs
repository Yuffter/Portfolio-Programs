using UniRx;
using UnityEngine;
using System;

namespace InGameScene {
    public class AirshipFallObserver : MonoBehaviour
    {
        private readonly Subject<Unit> _onAirshipFall = new Subject<Unit>();
        public IObservable<Unit> OnAirshipFall => _onAirshipFall;

        private const float FALL_THRESHOLD = -4.3f;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Observable.EveryUpdate()
                .TakeUntilDestroy(this)
                .Where(_ => transform.position.y < FALL_THRESHOLD)
                .First()
                .Subscribe(_ => _onAirshipFall.OnNext(Unit.Default))
                .AddTo(this);
        }

        public void OnDestroy()
        {
            _onAirshipFall.Dispose();
        }
    }
}