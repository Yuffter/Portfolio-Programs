using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Main.GameSystems.Models;
using Project.Main.GameSystems.Presentation;
using TMPro;
using UnityEngine;
using VContainer;
using KanKikuchi.AudioManager;
using UnityEngine.UI;
using unityroom.Api;

namespace Project.Main.HUD
{
    public class ResultView : MonoBehaviour, IResultPresentation
    {
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private Image _maskImage;
        [Inject] private ScoreModel _scoreModel;
        private const float _MAX_DURATION = 1.5f;
        [SerializeField] private Ease _ease = Ease.Linear;
        [SerializeField] private Material _dissolveMaterial;
        public async UniTask ShowResultAsync()
        {
            Sequence seq = DOTween.Sequence();
            await seq.Append(_dissolveMaterial.DOFloat(1, "_Threshold", 1))
               .AppendCallback(() =>
               {
                   BGMManager.Instance.Play(BGMPath.RESULT, 1, 0, 1, false);
                   _maskImage.gameObject.SetActive(true);
               })
               .Append(DOTween.To(
                () => 0,
                x => _resultText.text = $"スコア : {x}",
                _scoreModel.Score.CurrentValue, _MAX_DURATION
                )
                .SetEase(_ease))
                .Append(_maskImage.DOFillAmount(1, 1))
                .AppendCallback(() => UnityroomApiClient.Instance.SendScore(1, _scoreModel.Score.CurrentValue, ScoreboardWriteMode.Always))
                .WithCancellation(this.GetCancellationTokenOnDestroy());
        }

        private void Start()
        {
            _dissolveMaterial.SetFloat("_Threshold", 0);
            _resultText.SetText("");
        }
    }
}