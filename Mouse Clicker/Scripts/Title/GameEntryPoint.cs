using Project.Title.GameSystems;
using UnityEngine;
using VContainer.Unity;
using Cysharp.Threading.Tasks;

namespace Project.Title
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
