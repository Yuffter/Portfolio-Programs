using UnityEngine;

namespace InGame {
    public class ServiceRegisterer : MonoBehaviour
    {
        [SerializeField] private SO.EventHub.GuideEventHub _guideEventHub;
        [SerializeField] private SO.CurrentStageData _currentStageData;
        [SerializeField] private SO.StageDatabase _stageDatabase;
        [SerializeField]private SO.EventHub.ConsumablesEventHub _consumablesEventHub;
        [SerializeField] private RunTimePlayerBaseSO _runTimePlayerBaseSO;
        [SerializeField] private SO.EventHub.PopupTextEventHub _popupTextEventHub;
        private void Awake()
        {
            ServiceLocator.Register(FindAnyObjectByType<PlayerStopManager>());
            ServiceLocator.Register(FindAnyObjectByType<PullPlayer>());
            ServiceLocator.Register(new Player.HoverEffectFacade());
            ServiceLocator.Register(FindAnyObjectByType<FieldCamera.Controller>());
            ServiceLocator.Register(FindAnyObjectByType<Animation.GameStartAnimator>());
            ServiceLocator.Register(FindAnyObjectByType<Animation.GameFinishAnimator>());
            ServiceLocator.Register<Result.IFacade>(FindAnyObjectByType<Result.Facade>());
            ServiceLocator.Register(FindAnyObjectByType<ItemFacade>());
            ServiceLocator.Register(FindAnyObjectByType<Animations.BackToStageSelectButtonAnimator>());
            ServiceLocator.Register<Animation.TurnTextAnimator>(FindAnyObjectByType<Animation.TurnTextAnimator>());
            ServiceLocator.Register(_guideEventHub);
            ServiceLocator.Register(_currentStageData);
            ServiceLocator.Register(_stageDatabase);
            ServiceLocator.Register(_consumablesEventHub);
            ServiceLocator.Register(_runTimePlayerBaseSO);
            ServiceLocator.Register(_popupTextEventHub);
        }

        private void OnDisable()
        {
            ServiceLocator.UnRegister<PlayerStopManager>();
            ServiceLocator.UnRegister<PullPlayer>();
            ServiceLocator.UnRegister<Player.HoverEffectFacade>();
            ServiceLocator.UnRegister<FieldCamera.Controller>();
            ServiceLocator.UnRegister<Animation.GameStartAnimator>();
            ServiceLocator.UnRegister<Animation.GameFinishAnimator>();
            ServiceLocator.UnRegister<Result.IFacade>();
            ServiceLocator.UnRegister<ItemFacade>();
            ServiceLocator.UnRegister<Animations.BackToStageSelectButtonAnimator>();
            ServiceLocator.UnRegister<Animation.TurnTextAnimator>();
            ServiceLocator.UnRegister<SO.EventHub.GuideEventHub>();
            ServiceLocator.UnRegister<SO.CurrentStageData>();
            ServiceLocator.UnRegister<SO.StageDatabase>();
            ServiceLocator.UnRegister<SO.EventHub.ConsumablesEventHub>();
            ServiceLocator.UnRegister<RunTimePlayerBaseSO>();
            ServiceLocator.UnRegister<SO.EventHub.PopupTextEventHub>();
        }
    }
}
