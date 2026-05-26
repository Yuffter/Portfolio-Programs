using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAnimationBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float _sizeDelta = 0.5f;

    /// <summary>
    /// マウスがボタンに触れたときの処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill(true);
        transform.DOScale(_sizeDelta, 0.1f).SetEase(Ease.InOutSine).SetLink(gameObject).SetRelative();
    }

    /// <summary>
    /// マウスがボタンから離れたときの処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill(true);
        transform.DOScale(-_sizeDelta, 0.1f).SetEase(Ease.InOutSine).SetLink(gameObject).SetRelative();
    }

    /// <summary>
    /// 差分を変更する
    /// </summary>
    /// <param name="sizeDelta"></param>
    public void SetSizeDelta(float sizeDelta) {
        _sizeDelta = sizeDelta;
    }
}
