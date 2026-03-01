using UnityEngine;

namespace StageSelect
{
    public class ServiceRegisterer : MonoBehaviour
    {
        [SerializeField] private SO.StageDatabase _stageDatabase;
        [SerializeField] private SO.CurrentStageData _currentStageData;
        void Awake()
        {
            ServiceLocator.Register(FindAnyObjectByType<StagePanel.Factory.Factory>());
            ServiceLocator.Register(FindAnyObjectByType<StagePanel.Manager>());
            ServiceLocator.Register(FindAnyObjectByType<Connector.StageChangeConnector>());
            ServiceLocator.Register(FindAnyObjectByType<StageChanger.Controller>());
            ServiceLocator.Register(new StagePanel.IndexCalculator());
            ServiceLocator.Register(_stageDatabase);
            ServiceLocator.Register(_currentStageData);
        }

        void OnDisable()
        {
            ServiceLocator.UnRegister<StagePanel.Factory.Factory>();
            ServiceLocator.UnRegister<StagePanel.Manager>();
            ServiceLocator.UnRegister<Connector.StageChangeConnector>();
            ServiceLocator.UnRegister<StageChanger.Controller>();
            ServiceLocator.UnRegister<StagePanel.IndexCalculator>();
            ServiceLocator.UnRegister<SO.StageDatabase>();
            ServiceLocator.UnRegister<SO.CurrentStageData>();
        }
    }
}
