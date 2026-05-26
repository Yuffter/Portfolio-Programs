using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using KanKikuchi.AudioManager;

namespace TitleScene {
    public class ChangingCountButtonView : ButtonAnimationBase, IPointerClickHandler
    {
        [SerializeField, Header("変化量")]
        private int _amountOfChange = 1;

        [SerializeField]
        private TextMeshProUGUI _groupCountText;

        private void Start() {
            GameData.Instance.CurrentGroupCount = 1;
            _groupCountText.text = GameData.Instance.CurrentGroupCount.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            transform.DOKill(true);
            transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.2f, 10, 1).SetEase(Ease.InOutSine).SetLink(gameObject);

            SEManager.Instance.Play(SEPath.SELECT_CHANGING_BUTTON);

            GameData.Instance.CurrentGroupCount = Mathf.Clamp(
                GameData.Instance.CurrentGroupCount + _amountOfChange, 
                GameData.Instance.MinGroupCount, 
                GameData.Instance.MaxGroupCount
            );
            _groupCountText.text = GameData.Instance.CurrentGroupCount.ToString();
        }
    }
}