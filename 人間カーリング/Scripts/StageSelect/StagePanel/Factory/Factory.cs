using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StageSelect.StagePanel.Factory
{
    public class Factory : MonoBehaviour
    {
        [SerializeField] private StagePanel.Controller _stagePanelPrefab;
        [SerializeField] private SO.StageDatabase _stageDatabase;
        [SerializeField] private Transform _parentTransform;
        [SerializeField] private Image _thumbnailImage;
        private List<StagePanel.Controller> _stagePanels = new List<StagePanel.Controller>();

        [SerializeField, Header("ステージパネル間の間隔")] private float _merginX;
        /// <summary>
        /// データベースにあるステージ分ステージパネルを生成する
        /// </summary>
        /// <returns></returns>
        public List<StagePanel.Controller> Create()
        {
            for (int i = 0;i < _stageDatabase.Stages.Count;i++)
            {
                StagePanel.Controller stagePanel = Instantiate(_stagePanelPrefab, _parentTransform);
                stagePanel.transform.localPosition = new Vector3(i * _merginX, 0, 0);

                stagePanel.SetThumbnail(_stageDatabase.Stages[i].Thumbnail);
                stagePanel.SetHighScore(_stageDatabase.Stages[i].HighScore);

                _stagePanels.Add(stagePanel);
            }
            return _stagePanels;
        }
    }
}
