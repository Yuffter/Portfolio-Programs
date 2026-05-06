using KanKikuchi.AudioManager;
using Project.Main.GameSystems.ScriptableObjects;
using UnityEngine;

namespace Project.Main.Cursor
{
    public class Mover
    {
        private Transform _transform;
        private CursorSetting _cursorSetting;
        public Mover(Transform transform, CursorSetting cursorSetting)
        {
            _transform = transform;
            _cursorSetting = cursorSetting;
        }

        public Vector2Int Move(Vector2Int currentPosition, Vector2Int direction)
        {
            direction.y *= -1;
            if (!CanMove(currentPosition, direction)) return currentPosition;
            Debug.Log("Cursor moved to: " + (currentPosition + direction));

            _transform.position += Vector3.Scale((Vector3Int)direction, new Vector3(_cursorSetting.SpaceHorizontal, _cursorSetting.SpaceVertical));
            SEManager.Instance.Play(SEPath.CURSOR_MOVE);
            return currentPosition + direction;
        }

        public bool CanMove(Vector2Int currentPosition, Vector2 direction)
        {
            Vector2 newPosition = currentPosition + direction;
            return newPosition.x >= 0 && newPosition.x < _cursorSetting.MaxHorizontal &&
                   newPosition.y >= 0 && newPosition.y < _cursorSetting.MaxVertical;
        }
    }
}
