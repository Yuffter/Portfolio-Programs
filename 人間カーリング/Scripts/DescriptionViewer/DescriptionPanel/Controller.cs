using TMPro;
using UnityEngine;

namespace DescriptionViewer.DescriptionPanel
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private GameObject _panelObject;

        private void Awake()
        {
            _panelObject.SetActive(false);
        }

        /// <summary>
        /// 説明パネルを表示する
        /// </summary>
        /// <param name="description">説明文</param>
        /// <param name="position">表示位置(ワールド座標)</param>
        public void ShowDescription(string description, Vector3 position)
        {
            _descriptionText.text = description;
            // ワールド座標をスクリーン座標に変換してパネルの位置を設定
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
            _panelObject.transform.position = screenPosition + new Vector3(50,0,0); // 少し右にずらす
            _panelObject.SetActive(true);
        }

        public void HideDescription()
        {
            _panelObject.SetActive(false);
        }

        public bool IsActive => _panelObject.activeSelf;
    }
}
