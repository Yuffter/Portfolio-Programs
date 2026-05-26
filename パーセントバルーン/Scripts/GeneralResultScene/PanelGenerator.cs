using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GeneralResultScene
{
    /// <summary>
    /// パネル生成クラス
    /// </summary>
    public class PanelGenerator
    {
        private readonly PanelGeneratorConfig _panelGeneratorConfig;
        private readonly List<GroupPointData> _groupPointDataList;
        private int _currentRank = 0;
        private int _previousPoint = -1;

        public PanelGenerator(PanelGeneratorConfig panelGeneratorConfig, List<GroupPointData> groupPointDataList)
        {
            _panelGeneratorConfig = panelGeneratorConfig;
            _groupPointDataList = groupPointDataList;
        }

        public List<GameObject> GeneratePanel()
        {
            List<GameObject> panelList = new();

            for (int i = 0; i < GameData.Instance.CurrentGroupCount; i++)
            {
                var panel = GameObject.Instantiate(_panelGeneratorConfig.PanelPrefab, _panelGeneratorConfig.Canvas.transform);
                panel.transform.localPosition = new Vector3(-Screen.width*2, _panelGeneratorConfig.InitialPanelPositionY - i * _panelGeneratorConfig.PanelDistance, 0);
                if (_groupPointDataList[i].Point != _previousPoint)
                {
                    _currentRank++;
                    _previousPoint = _groupPointDataList[i].Point;
                }
                panel.GetComponent<TextMeshProUGUI>().text = $"{_currentRank}位 : <u>{_groupPointDataList[i].GroupNumber}班 ({_groupPointDataList[i].Point}点)</u>";
                if (_currentRank == 1)
                {
                    panel.GetComponent<TextMeshProUGUI>().color = Color.yellow;
                }
                else if (_currentRank == 2)
                {
                    panel.GetComponent<TextMeshProUGUI>().color = Color.gray;
                }
                else if (_currentRank == 3)
                {
                    panel.GetComponent<TextMeshProUGUI>().color = new Color(0.8f, 0.5f, 0.2f);
                }
                else
                {
                    panel.GetComponent<TextMeshProUGUI>().color = Color.white;
                }
                panelList.Add(panel);
            }

            return panelList;
        }
    }

    [System.Serializable]
    public class PanelGeneratorConfig {
        public GameObject PanelPrefab;
        public float PanelDistance;
        public float InitialPanelPositionY;
        public Canvas Canvas;
    }
}
