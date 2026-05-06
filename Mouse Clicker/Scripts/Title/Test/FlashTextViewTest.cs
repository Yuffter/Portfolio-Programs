using UnityEngine;
using VContainer.Unity;
using Project.Title.GameSystems.Presentation;

namespace Project.Title.Test
{
    public class FlashTextViewTest : IInitializable
    {
        private readonly IFlashTextPresentation _flashTextPresentation;

        public FlashTextViewTest(IFlashTextPresentation flashTextPresentation)
        {
            _flashTextPresentation = flashTextPresentation;
        }

        // Initialize is called once before the first execution of Update after the MonoBehaviour is created
        public void Initialize()
        {
            Debug.Log("FlashTextViewTest initialized.");
            _flashTextPresentation.StartAnimation();
        }
    }
}
