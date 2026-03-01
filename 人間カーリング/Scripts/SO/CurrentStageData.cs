using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/Create CurrentStageData")]
    public class CurrentStageData : ScriptableObject
    {
        public StageData Data { get; private set; }
        public int StageIndex { get; private set; }
        public void SetData(int stageIndex, StageData data)
        {
            Data = data;
            StageIndex = stageIndex;
            // 残りターン数を初期化
            data.CurrentTurnLeft = data.MaxTurnCount;
        }
    }
}
