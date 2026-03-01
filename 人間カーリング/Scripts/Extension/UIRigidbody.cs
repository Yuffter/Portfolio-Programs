using UnityEngine;

/// <summary>
/// UI用のRigidbodyコンポーネント
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class UIRigidbody : MonoBehaviour
{
    [SerializeField] private float _mass = 1f; // 質量
    public float Mass => _mass;

    [SerializeField] private Vector2 _velocity; // 速度
    public Vector2 Velocity => _velocity;
    
    [SerializeField] private Vector2 _acceleration; // 加速度
    public Vector2 Acceleration => _acceleration;

    [SerializeField] private float _angularVelocity; // 角速度
    public float AngularVelocity => _angularVelocity;

    [SerializeField] private Vector2 _angularAcceleration; // 角加速度
    public Vector2 AngularAcceleration => _angularAcceleration;

    [SerializeField] private float _inertia = 1f; // 慣性モーメント
    public float Inertia => _inertia;

    [SerializeField] public float GravityScale { get; set; } = 1f; // 重力スケール

    private RectTransform _rectTransform;
    private static readonly Vector2 Gravity = new Vector2(0, -1000f); // 重力加速度

    private Vector2 _force = Vector2.zero;
    private float _torque = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        if (_rectTransform == null)
        {
            Debug.LogError("UIRigidbody requires a RectTransform component.");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        // 重力の影響を加える(F = m * a(g))
        AddForce(Gravity * GravityScale * _mass);

        // 加速度を更新
        _acceleration = _force / _mass;
        // 速度を更新
        _velocity += _acceleration * dt;
        // 位置を更新
        _rectTransform.anchoredPosition += _velocity * dt;

        _force = Vector2.zero;

        // 角加速度の計算(トルク=慣性モーメント*角加速度)
        float angularAcceleration = _torque / _inertia;

        _angularVelocity += angularAcceleration * dt;

        float z = _rectTransform.localEulerAngles.z;
        z += _angularVelocity * dt;
        _rectTransform.localEulerAngles = new Vector3(0f, 0f, z);

        _torque = 0f;
    }

    public void AddForce(Vector2 force)
    {
        _force += force;
    }

    public void AddTorque(float torque)
    {
        _torque += torque;
    }
}
