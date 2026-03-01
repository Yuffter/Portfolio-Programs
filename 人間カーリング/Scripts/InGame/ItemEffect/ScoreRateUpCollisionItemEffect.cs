using UnityEngine;

namespace InGame.ItemEffect
{
    /// <summary>
    /// 衝突によって特典倍率が上がるアイテム効果
    /// </summary>
    public class ScoreRateUpCollisionItemEffect : ItemEffectBase
    {
        private GameObject _hitPlayer;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _hitPlayer = collision.gameObject;
                OnItemEffect();
            }
        }
        public override void OnItemEffect()
        {
            var playerBase = _hitPlayer.GetComponent<PlayerBase>();
            playerBase.SetScoreRate(playerBase._scoreRate * 2f);
            ServiceLocator.Resolve<SO.EventHub.PopupTextEventHub>().PopupRequestedEvent.Raise(new SO.Event.PopupRequest("スコア倍率x2!", _hitPlayer.transform.position, Color.yellow));
        }
    }
}
