using UnityEngine;
using DG.Tweening;

namespace TitleScene
{
    public class BalloonMovement : MonoBehaviour
    {
        [SerializeField] private float _deltaY = 2;
        [SerializeField] private float _duration = 3;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            transform
            .DOMoveY(_deltaY, _duration)
            .SetRelative()
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetLink(gameObject);
        }
    }
}