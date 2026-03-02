using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using KanKikuchi.AudioManager;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Sequence movement;
    [SerializeField] GameObject deadEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMoveRightAnimation() {
        movement = DOTween.Sequence();
        movement.Append(transform.DORotate(new Vector3(0,0,-20), PlayerStatus.Instance.moveDuration / 2f).SetEase(Ease.Linear))
        .Append(transform.DORotate(new Vector3(0,0,0),PlayerStatus.Instance.moveDuration / 2f).SetEase(Ease.Linear));
    }

    public void PlayMoveLeftAnimation() {
        movement = DOTween.Sequence();
        movement.Append(transform.DORotate(new Vector3(0,0,20), PlayerStatus.Instance.moveDuration / 2f).SetEase(Ease.Linear))
        .Append(transform.DORotate(new Vector3(0,0,0),PlayerStatus.Instance.moveDuration / 2f).SetEase(Ease.Linear));
    }

    public void PlayMoveUpAnimation() {
        movement = DOTween.Sequence();
        movement.Append(transform.DORotate(new Vector3(-20,0,0), PlayerStatus.Instance.moveDuration / 2f).SetEase(Ease.Linear))
        .Append(transform.DORotate(new Vector3(0,0,0),PlayerStatus.Instance.moveDuration / 2f).SetEase(Ease.Linear));
    }

    public void PlayMoveDownAnimation() {
        movement = DOTween.Sequence();
        movement.Append(transform.DORotate(new Vector3(20,0,0), PlayerStatus.Instance.moveDuration / 2f).SetEase(Ease.Linear))
        .Append(transform.DORotate(new Vector3(0,0,0),PlayerStatus.Instance.moveDuration / 2f).SetEase(Ease.Linear));
    }

    public Sequence PlayDeadAnimation() {
        Sequence seq = DOTween.Sequence();
        return seq.AppendInterval(2f)
        .AppendCallback(() => {
            for (int i = 0;i < 1;i++) {
                Vector3 diff = new Vector3(UnityEngine.Random.Range(-1,1f),UnityEngine.Random.Range(-1,1f),UnityEngine.Random.Range(-1,1f));
                GameObject g = Instantiate(deadEffect,transform.position + diff,quaternion.identity);
                g.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            }
            SEManager.Instance.Play(SEPath.PLAYER_DEAD,1,0,0.5f);
            Destroy(gameObject);
        }).SetLink(gameObject);
    }
}
