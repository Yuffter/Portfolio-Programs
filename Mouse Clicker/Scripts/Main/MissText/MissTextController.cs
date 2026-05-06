using UnityEngine;

namespace Project.Main.MissText
{
    public class MissTextController : MonoBehaviour
    {
        private Rigidbody2D _rb;
        [SerializeField] private Vector2 _force;
        [SerializeField] private float _gravityScale;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.AddForce(_force, ForceMode2D.Impulse);
            _rb.gravityScale = _gravityScale;
        }
    }
}