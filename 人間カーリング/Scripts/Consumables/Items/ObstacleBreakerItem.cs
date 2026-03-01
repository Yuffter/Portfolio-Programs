using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Consumables.Items
{
    public class ObstacleBreakerItem : ItemBase
    {
        public override bool Use(Vector2 usePosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(usePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.CompareTag("Obstacles"))
                {
                    hitInfo.collider.GetComponent<ObstacleLifeCycle>().DestroyForced();
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
                if (hitInfo.collider.CompareTag("Obstacles"))
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
    }
}
