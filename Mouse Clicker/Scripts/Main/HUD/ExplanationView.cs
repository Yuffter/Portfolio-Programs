using Project.Main.GameSystems.Presentation;
using UnityEngine;

namespace Project.Main.HUD
{
    public class ExplanationView : MonoBehaviour, IExplanationPresentation
    {
        [SerializeField] private GameObject _explanationPanel;
        public void HideExplanation()
        {
            _explanationPanel.SetActive(false);
        }

        public void ShowExplanation()
        {
            _explanationPanel.SetActive(true);
        }
    }
}