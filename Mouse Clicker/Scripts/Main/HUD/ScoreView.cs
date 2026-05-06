using Project.Main.GameSystems.Models;
using Project.Main.GameSystems.Presentation;
using R3;
using TMPro;
using Unity.Collections;
using UnityEngine;
using VContainer;

namespace Project.Main.HUD
{
    public class ScoreView : MonoBehaviour, IScorePresentation
    {
        [SerializeField] private TMP_Text _scoreText;

        public void UpdateText(int score)
        {
            _scoreText.SetText($"スコア : {score}");
        }
    }
}