using R3;
using UnityEngine;

namespace Consumables.ItemSlot.QuantityText
{
    /// <summary>
    /// 消耗品の所持数表示Presenter
    /// </summary>
    public class QuantityTextPresenter : MonoBehaviour
    {
        [SerializeField] private DependencyContainer _dependencyContainer;
        [SerializeField] private QuantityTextView _view;

        private void Awake()
        {
            _dependencyContainer.QuantityModel.Quantity.Subscribe(OnQuantityChanged).AddTo(this);
        }

        private void OnQuantityChanged(int newQuantity)
        {
            _view.SetQuantityText(newQuantity);
        }
    }
}
