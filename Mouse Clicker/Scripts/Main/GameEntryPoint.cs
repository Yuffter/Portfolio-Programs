using UnityEngine;
using VContainer;
using VContainer.Unity;
using Project.Main.GameSystems;
using Cysharp.Threading.Tasks;

namespace Project.Main
{
    public class GameEntryPoint : IInitializable
    {
        private readonly GameSystem _gameSystem;

        public GameEntryPoint(GameSystem gameSystem)
        {
            _gameSystem = gameSystem;
        }

        public void Initialize()
        {
            _gameSystem.Initialize().Forget();
        }
    }
}