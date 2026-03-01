using UnityEngine;
using UnityEngine.InputSystem;

namespace Consumables.Items
{
    public class ScoreAttackUpItem : ItemBase
    {
        private const string PLAYER_TAG = "Player";
        private const float ROTATE_SPEED = 90f;
        public override bool Use(Vector2 usePosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(usePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.CompareTag(PLAYER_TAG))
                {
                    var playerBase = hitInfo.collider.GetComponent<PlayerBase>();
                    playerBase.SetAttack(playerBase._Attack * 2f);
                    playerBase.SetScoreRate(playerBase._scoreRate * 2f);
                    return true;
                }
            }
            return false;
        }

        public override void OnDrag()
        {
            // ここではサンプルとして対象物に近づいたら大きくする処理を入れる
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.CompareTag(PLAYER_TAG))
                {
                    transform.localScale = Vector3.one * 0.3f;
                }
                else
                {
                    transform.localScale = Vector3.one * 0.2f;
                }
            }
            else
            {
                transform.localScale = Vector3.one * 0.2f;
            }
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, ROTATE_SPEED * Time.deltaTime);
        }
    }
}
