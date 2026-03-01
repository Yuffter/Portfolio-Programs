using System;
using AudioManager.SE;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Consumables.Follower
{
    /// <summary>
    /// 消耗品のドラッグ中に表示されるオブジェクトを制御するクラス
    /// </summary>
    public class Controller : MonoBehaviour
    {
        [SerializeField, Header("追従させるTransform")] private Transform _followTransform;
        [SerializeField, Header("オフセット")] private Vector2 _offset;
        [SerializeField, Header("カメラからの距離")] private float _distanceFromCamera = 10f;

        private GameObject _followChildObject = null;
        private Items.ItemBase _currentItem => _followChildObject?.GetComponent<Items.ItemBase>();

        public event Action<GameObject> OnSetEvent;
        public event Func<Vector2, bool> OnUseEvent;
        public event Action OnDragEvent;

        private void Awake()
        {
            OnSetEvent += Set;
            OnUseEvent += Use;
            OnDragEvent += OnDrag;
        }

        private void OnDestroy()
        {
            OnSetEvent = null;
            OnUseEvent = null;
            OnDragEvent = null;
        }

        public void NotifySet(GameObject followObject)
        {
            OnSetEvent?.Invoke(followObject);
        }

        public bool NotifyUse(Vector2 usePosition)
        {
            if (OnUseEvent != null)
            {
                return OnUseEvent.Invoke(usePosition);
            }
            return false;
        }

        public void NotifyOnDrag()
        {
            OnDragEvent?.Invoke();
        }

        /// <summary>
        /// アイテムをセットする
        /// </summary>
        /// <param name="followObject">表示するオブジェクト</param>
        private void Set(GameObject followObject)
        {
            _followChildObject = Instantiate(followObject, _followTransform);
            _followChildObject.transform.localPosition = Vector3.zero;
        }

        /// <summary>
        /// 消耗品を使用する
        /// <para>消耗品が使用出来た場合はtrueを返す</para>
        /// </summary>
        private bool Use(Vector2 usePosition)
        {
            if (_followChildObject != null && _currentItem != null)
            {
                // 消耗品が使用できる場合は使用する
                bool canUse = _currentItem.Use(usePosition);

                Destroy(_followChildObject);
                _followChildObject = null;
                return canUse;
            }

            return false;
        }

        /// <summary>
        /// 消耗品のドラッグ中の処理
        /// </summary>
        private void OnDrag()
        {
            _currentItem?.OnDrag();

            // カメラから一定の距離を保って追従させる
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _distanceFromCamera));
            _followTransform.position = worldPosition + (Vector3)_offset;
        }
    }
}
