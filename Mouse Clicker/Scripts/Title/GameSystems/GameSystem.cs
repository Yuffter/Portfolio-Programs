using Cysharp.Threading.Tasks;
using Project.Title.GameSystems.Sequences;
using UnityEngine;

namespace Project.Title.GameSystems
{
    public class GameSystem
    {
        private readonly MainSequence _mainSequence;
        private readonly BeginningSequence _beginningSequence;

        public GameSystem(MainSequence mainSequence, BeginningSequence beginningSequence)
        {
            _mainSequence = mainSequence;
            _beginningSequence = beginningSequence;
        }

        public async UniTask Initialize()
        {
            await _mainSequence.StartSequence();
            await _beginningSequence.StartSequence();
        }
    }
}