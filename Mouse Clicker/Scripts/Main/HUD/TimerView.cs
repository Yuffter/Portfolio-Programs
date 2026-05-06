using Project.Main.GameSystems.Presentation;
using TMPro;
using UnityEngine;

namespace Project.Main.HUD
{
    /// <summary>
    /// タイマーの表示を担当するクラス
    /// </summary>
    public class TimerView : MonoBehaviour, ITimerPresentation
    {
        [SerializeField] private TMP_Text _timerText;
        public void UpdateTimerText(int time)
        {
            _timerText.text = $"残り時間 : {time}秒";
        }
    }
}