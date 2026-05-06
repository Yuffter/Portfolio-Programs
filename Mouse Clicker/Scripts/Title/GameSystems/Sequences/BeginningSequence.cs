using UnityEngine;
using Cysharp.Threading.Tasks;
using Project.Title.GameSystems.Presentation;
using KanKikuchi.AudioManager;
using System;
using UnityEngine.SceneManagement;

namespace Project.Title.GameSystems.Sequences {
    public class BeginningSequence {
        private readonly ITransitionPresentation _transitionPresentation;

        public BeginningSequence(ITransitionPresentation transitionPresentation) {
            _transitionPresentation = transitionPresentation;
        }

        public async UniTask StartSequence()
        {
            SEManager.Instance.Play(SEPath.DECISION);
            await UniTask.Delay(TimeSpan.FromSeconds(1));

            BGMManager.Instance.FadeOut(BGMPath.TITLE, 1f);
            await _transitionPresentation.StartTransitionAsync();
            SceneManager.LoadScene("Main");
        }
    }
}