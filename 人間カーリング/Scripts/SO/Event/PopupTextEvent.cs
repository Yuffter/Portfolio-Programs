using UnityEngine;

namespace SO.Event
{
    [CreateAssetMenu(menuName = "ScriptableObject/Events/Create Popup Text Event")]
    public class PopupTextEvent : GameEvent<PopupRequest> {}

    public struct PopupRequest
    {
        public string Text;
        public Vector3 Position;
        public Color Color;

        public PopupRequest(string text, Vector3 position, Color color)
        {
            Text = text;
            Position = position;
            Color = color;
        }

        /// <summary>
        /// colorHexは"#RRGGBB"形式の文字列を想定。例: "#FF0000"は赤色。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="colorHex"></param>
        public PopupRequest(string text, Vector3 position, string colorHex)
        {
            Text = text;
            Position = position;
            if (ColorUtility.TryParseHtmlString(colorHex, out Color color))
            {
                Color = color;
            }
            else
            {
                Debug.LogWarning($"Invalid color hex string: {colorHex}. Defaulting to white.");
                Color = Color.white;
            }
        }
    }
}
