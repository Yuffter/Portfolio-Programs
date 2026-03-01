using UnityEngine;

namespace Title
{
    public class ServiceRegiserer : MonoBehaviour
    {
        [SerializeField] private SO.StageDatabase _stageDatabase;
        void Awake()
        {
            ServiceLocator.Register(FindAnyObjectByType<MainAnimator>());
            ServiceLocator.Register(_stageDatabase);
        }

        void OnDisable()
        {
            ServiceLocator.UnRegister<MainAnimator>();
            ServiceLocator.UnRegister<SO.StageDatabase>();
        }
    }
}
