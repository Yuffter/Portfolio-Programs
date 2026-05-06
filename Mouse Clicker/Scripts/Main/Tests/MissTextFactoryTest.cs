using Project.Main.Factory;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Project.Main.Tests
{
    public class MissTextFactoryTest : IInitializable
    {
        private readonly MissTextFactory _missTextFactory;

        public MissTextFactoryTest(MissTextFactory missTextFactory)
        {
            _missTextFactory = missTextFactory;
        }
        public void Initialize()
        {
            Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.Space))
            .Subscribe(_ => _missTextFactory.CreateMissText(new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0)));
        }
    }
}