using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclingEnemyBulletController : MonoBehaviour,IBullet
{
    [SerializeField] float moveSpeed = 0.005f;
    [SerializeField] float distanceFromCenter = 0.3f;
    public float Angle;
    public float Radius;
    public Vector3 CenterPosition;
    public float EulerAngleY;

    public float damageAmount {get;set;} = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            CenterPosition.x + Radius * Mathf.Cos((EulerAngleY + Angle) * Mathf.Deg2Rad),
            CenterPosition.y + distanceFromCenter,
            CenterPosition.z + Radius * Mathf.Sin((EulerAngleY + Angle) * Mathf.Deg2Rad)
        );

        Radius += moveSpeed;

        if (Radius >= 40) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            FindAnyObjectByType<PlayerHPModel>().Damage(damageAmount);
        }
    }
}
