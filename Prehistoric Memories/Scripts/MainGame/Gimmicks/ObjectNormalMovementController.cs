using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Diagnostics;

public class ObjectNormalMovementController : MonoBehaviour
{
    [SerializeField] ObjectNormalMovementData objectNormalMovementData;
    Sequence seq;
    // Start is called before the first frame update
    void Start()
    {
        seq = DOTween.Sequence();
        SetSequence();
        seq.SetLoops(-1);
    }

    void SetSequence()
    {
        foreach (var normalMovementData in objectNormalMovementData.movementData)
        {
            switch (normalMovementData.movementType)
            {
                case MovementType.POSITION:
                    switch (normalMovementData.connectType)
                    {
                        case ConnectType.APPEND:
                            if (normalMovementData.isRelative)
                                seq.Append(transform.DOMove(normalMovementData.position, normalMovementData.duration)
                                    .SetEase(normalMovementData.ease)
                                    .SetRelative());
                            else
                                seq.Append(transform.DOMove(normalMovementData.position, normalMovementData.duration)
                                    .SetEase(normalMovementData.ease));
                            break;
                        case ConnectType.JOIN:
                            if (normalMovementData.isRelative)
                                seq.Join(transform.DOMove(normalMovementData.position, normalMovementData.duration)
                                    .SetEase(normalMovementData.ease)
                                    .SetRelative());
                            else
                                seq.Join(transform.DOMove(normalMovementData.position, normalMovementData.duration)
                                    .SetEase(normalMovementData.ease));
                            break;
                    }
                    break;
                case MovementType.ROTATION:
                    switch (normalMovementData.connectType)
                    {
                        case ConnectType.APPEND:
                            if (normalMovementData.isRelative)
                                seq.Append(transform.DORotate(normalMovementData.rotation, normalMovementData.duration)
                                    .SetEase(normalMovementData.ease)
                                    .SetRelative());
                            else
                                seq.Append(transform.DORotate(normalMovementData.rotation, normalMovementData.duration)
                                    .SetEase(normalMovementData.ease));
                            break;
                        case ConnectType.JOIN:
                            if (normalMovementData.isRelative)
                                seq.Join(transform.DORotate(normalMovementData.rotation, normalMovementData.duration)
                                    .SetEase(normalMovementData.ease)
                                    .SetRelative());
                            else
                                seq.Join(transform.DORotate(normalMovementData.rotation, normalMovementData.duration)
                                    .SetEase(normalMovementData.ease));
                            break;
                    }
                    break;
                case MovementType.SCALE:
                    switch (normalMovementData.connectType)
                    {
                        case ConnectType.APPEND:
                            if (normalMovementData.isRelative)
                                seq.Append(transform.DOScale(normalMovementData.scale, normalMovementData.duration)
                                    .SetEase(normalMovementData.ease)
                                    .SetRelative());
                            else
                                seq.Append(transform.DOScale(normalMovementData.scale, normalMovementData.duration)
                                    .SetEase(normalMovementData.ease));
                            break;
                        case ConnectType.JOIN:
                            if (normalMovementData.isRelative)
                                seq.Join(transform.DOScale(normalMovementData.scale, normalMovementData.duration)
                                    .SetEase(normalMovementData.ease)
                                    .SetRelative());
                            else
                                seq.Join(transform.DOScale(normalMovementData.scale, normalMovementData.duration)
                                    .SetEase(normalMovementData.ease));
                            break;
                    }
                    break;
                case MovementType.COOLDOWN:
                    seq.AppendInterval(normalMovementData.cooldownDuration);
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) collision.collider.transform.parent = transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.parent = null;
            collision.collider.transform.eulerAngles = Vector3.zero;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) collision.collider.transform.eulerAngles = Vector3.zero;
    }
}
