using UnityEngine;
using UnityEngine.Pool;

namespace PopupText
{
    public class PopupPool : MonoBehaviour
    {
        [SerializeField] private PopupAnimator _popupPrefab;
        [SerializeField] private Transform _popupParent;

        private ObjectPool<PopupAnimator> _pool;

        void Awake()
        {
            _pool = new ObjectPool<PopupAnimator>(
                createFunc: () => Instantiate(_popupPrefab, _popupParent),
                actionOnGet: (obj) => obj.gameObject.SetActive(true),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj.gameObject),
                collectionCheck: false,
                defaultCapacity: 10,
                maxSize: 20
            );
        }

        public PopupAnimator Get()
        {
            return _pool.Get();
        }

        public void Release(PopupAnimator obj)
        {
            _pool.Release(obj);
        }

        private void OnDestroy()
        {
            _pool.Dispose();
            _pool = null;
        }
    }
}
