using R3;
using UnityEngine;

namespace StageSelect.StageChanger
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Button _leftArrow;
        [SerializeField] private UnityEngine.UI.Button _rightArrow;
        private StagePanel.IndexCalculator _indexCalculator;

        public void Initialize(StagePanel.IndexCalculator indexCalculator)
        {
            _indexCalculator = indexCalculator;
            _indexCalculator.CurrentIndex.Subscribe(index =>
            {
                if (index == _indexCalculator.MinIndex)
                {
                    DisableLeftArrow();
                }
                else
                {
                    EnableLeftArrow();
                }

                if (index == _indexCalculator.MaxIndex)
                {
                    DisableRightArrow();
                }
                else
                {
                    EnableRightArrow();
                }
            }).AddTo(this);
        }

        private void EnableLeftArrow()
        {
            _leftArrow.interactable = true;
        }

        private void DisableLeftArrow()
        {
            _leftArrow.interactable = false;
        }

        private void EnableRightArrow()
        {
            _rightArrow.interactable = true;
        }

        private void DisableRightArrow()
        {
            _rightArrow.interactable = false;
        }
    }
}
