using DG.Tweening;
using UnityEngine;

namespace MinutesGame.DX.REC
{
    public class RECController : MonoBehaviour
    {
        private const float CAMERA_DISTANCE = 0.5f;
        private readonly Vector3 MONITOR_POSITION = new Vector3(-1.38f,0.43f, -1.05f);
        private readonly Vector3 MONITOR_SCALE = new Vector3(0.15f,0.15f,0.15f);
        private readonly Vector3 SHINE_MASK_TARGET_POSITION = new Vector3(1.96f,-4.42f,0.16f);
        private Vector3 _initialPosition;
        private Vector3 _initialEulerAngles;

        [SerializeField]
        private Camera _mainCamera;
        [SerializeField]
        private Transform _shineMaskTransform;
        [SerializeField]
        private Animator _recAnimator;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
            _initialPosition = _mainCamera.transform.position + _mainCamera.transform.forward * CAMERA_DISTANCE - new Vector3(0f,0.1f,0f);
            _initialEulerAngles = _mainCamera.transform.eulerAngles;
            transform.localScale = Vector3.zero;
            transform.position = _initialPosition;
        }

        public void MoveToMonitorPosition()
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOLocalRotate(new Vector3(20,-180,360 * 10), 2f, RotateMode.FastBeyond360).SetEase(Ease.OutCubic));
            seq.Join(transform.DOScale(MONITOR_SCALE * 1.5f, 2f).SetEase(Ease.OutCubic));
            seq.Append(_shineMaskTransform.DOLocalMove(SHINE_MASK_TARGET_POSITION, 1f).SetEase(Ease.OutCubic));
            // seq.AppendInterval(1f);
            seq.Append(transform.DOLocalMove(MONITOR_POSITION, 1f).SetEase(Ease.OutBounce));
            seq.Join(transform.DOScale(MONITOR_SCALE, 1f).SetEase(Ease.OutBounce));
            seq.Join(transform.DORotate(_initialEulerAngles, 1f).SetEase(Ease.OutCubic));
            seq.AppendCallback(() => _recAnimator.SetTrigger("Start"));
        }
    }
}