using Consumables.ItemSlot.QuantityText;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Consumables.ItemSlot
{
    public class DependencyContainer : MonoBehaviour
    {
        [SerializeField, Header("アイテムアイコン")] private Image _iconImage;
        /// <summary>
        /// アイテムアイコン
        /// </summary>
        public Image IconImage => _iconImage;

        [SerializeField, Header("消耗品データベース")] private SO.Consumables.ConsumablesDatabase _consumablesDatabase;
        /// <summary>
        /// 消耗品データベース
        /// </summary>
        public SO.Consumables.ConsumablesDatabase ConsumablesDatabase => _consumablesDatabase;
        [SerializeField, Header("ドラッグハンドラー")] private ItemDragHandler _itemDragHandler;
        /// <summary>
        /// ドラッグハンドラー
        /// </summary>
        public ItemDragHandler ItemDragHandler => _itemDragHandler;

        private ConsumableData _consumableData;
        /// <summary>
        /// 消耗品データ
        /// </summary>
        public ConsumableData ConsumableData => _consumableData;
        private QuantityModel _quantityModel;
        /// <summary>
        /// 所持数モデル
        /// </summary>
        public QuantityModel QuantityModel => _quantityModel;

        private MainController _mainController;
        /// <summary>
        /// メインコントローラー
        /// </summary>
        public MainController MainController => _mainController;

        private void Awake()
        {
            _consumableData = new ConsumableData();
            _quantityModel = new QuantityModel();
            _mainController = FindAnyObjectByType<MainController>();
        }
    }
}
