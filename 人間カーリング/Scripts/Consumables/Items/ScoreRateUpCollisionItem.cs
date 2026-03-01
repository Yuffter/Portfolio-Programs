using UnityEngine;
using UnityEngine.InputSystem;

namespace Consumables.Items
{
    public class ScoreRateUpCollisionItem : ItemBase
    {
        private const string PLAYER_NAME = "Player";
        private float _rotationSpeed = 90f;
        public override bool Use(Vector2 usePosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(usePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.CompareTag(PLAYER_NAME))
                {
                    hitInfo.collider.gameObject.AddComponent<InGame.ItemEffect.ScoreRateUpCollisionItemEffect>();
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
                if (hitInfo.collider.CompareTag(PLAYER_NAME))
                {
                    transform.localScale = Vector3.one * 0.5f;
                }
                else
                {
                    transform.localScale = Vector3.one * 0.3f;
                }
            }
            else
            {
                transform.localScale = Vector3.one * 0.3f;
            }
        }

        private void Update()
        {
            transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
        }
    }
}
