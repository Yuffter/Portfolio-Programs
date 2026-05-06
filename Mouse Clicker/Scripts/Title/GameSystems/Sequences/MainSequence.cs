using System;
using Cysharp.Threading.Tasks;
using KanKikuchi.AudioManager;
using Project.Title.GameSystems.Presentation;
using UnityEngine;

namespace Project.Title.GameSystems.Sequences
{
    public class MainSequence
    {
        private readonly IFlashTextPresentation _flashTextPresentation;
        public MainSequence(IFlashTextPresentation flashTextPresentation)
        {
            _flashTextPresentation = flashTextPresentation;
        }

        public async UniTask StartSequence()
        {
            BGMManager.Instance.Play(BGMPath.TITLE);
            _flashTextPresentation.StartAnimation();

            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        }
    }

}