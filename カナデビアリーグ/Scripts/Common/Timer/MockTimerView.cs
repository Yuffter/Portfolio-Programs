using UnityEngine;

namespace MinutesGame.Common.Timer
{
    public class MockTimerView : ITimerView
    {
        public void SetTimerText(float time)
        {
            Debug.Log($"Timer: {time}");
        }

        public void Initialize(float maxTime)
        {
            Debug.Log($"Timer initialized with max time: {maxTime}");
        }
    }
}