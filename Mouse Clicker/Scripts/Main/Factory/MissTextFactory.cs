using UnityEngine;

namespace Project.Main.Factory
{
    public class MissTextFactory : MonoBehaviour
    {
        [SerializeField] private GameObject _missTextPrefab;
        [SerializeField] private Transform _worldCanvas;
        [SerializeField] private float _space = 1;

        /// <summary>
        /// ミステキストを生成する
        /// </summary>
        /// <param name="position">ワールド座標</param>
        /// <returns></returns>
        public GameObject CreateMissText(Vector3 position)
        {
            GameObject missText = Instantiate(_missTextPrefab, _worldCanvas);

            // UI座標に変換
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _worldCanvas as RectTransform,
                UnityEngine.Camera.main.WorldToScreenPoint(position + Vector3.right * _space),
                UnityEngine.Camera.main,
                out Vector2 localPoint
            );
            missText.transform.localPosition = localPoint;
            return missText;
        }
    }
}