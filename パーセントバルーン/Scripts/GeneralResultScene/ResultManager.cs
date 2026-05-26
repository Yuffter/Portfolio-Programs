using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GeneralResultScene
{
    /// <summary>
    /// 結果管理クラス
    /// </summary>
    public class ResultManager : MonoBehaviour
    {
        [SerializeField, Header("生成設定")]
        private PanelGeneratorConfig _panelGeneratorConfig;

        [SerializeField, Header("アニメーション設定")]
        private PanelAnimatorConfig _panelAnimatorConfig;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            /* 総合得点の計算 */
            PointCalculator pointCalculator = new PointCalculator();
            List<GroupPointData> groupsData = pointCalculator.CalculatePoint();

            /* パネル生成 */
            PanelGenerator panelGenerator = new PanelGenerator(_panelGeneratorConfig, groupsData);
            _panelAnimatorConfig.PanelList = panelGenerator.GeneratePanel();

            /* パネルアニメーション */
            PanelAnimator panelAnimator = new PanelAnimator(_panelAnimatorConfig);
            panelAnimator.Animate().Forget();
        }
    }
}