using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using KanKikuchi.AudioManager;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverAnimation : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerShooting playerShooting;
    [SerializeField] PlayerHPEffectView playerHPEffectView;
    [SerializeField] PlayerRigid playerRigid;
    [SerializeField] PlayerAnimation playerAnimation;

    [SerializeField] Image gameOverTitle;
    [SerializeField] TMP_Text continueLabel;
    [SerializeField] Material screenDissolve;

    bool canContinue = false;
    // Start is called before the first frame update
    void Start()
    {
        gameOverTitle.fillAmount = 0f;
        continueLabel.alpha = 0;
        screenDissolve.SetFloat("_Progress",0f);

        this.UpdateAsObservable()
            .Where(_ => canContinue == true)
            .Where(_ => Input.GetKeyDown(KeyCode.Return))
            .First()
            .Subscribe(_ => {
                Conitnue();
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play() {
        Sequence MainSeq = DOTween.Sequence();
        MainSeq.AppendCallback(() => {
            playerMovement.gameObject.transform.Find("Collision").GetComponent<Collider>().enabled = false;
            playerMovement.UnableToMove();
            playerShooting.UnableToShoot();
            playerHPEffectView.CancelWarn();
            playerHPEffectView.DeadScreen();
            playerRigid.Crash();
        })
        .Append(playerAnimation.PlayDeadAnimation())
        .AppendInterval(2f)
        .Append(gameOverTitle.DOFillAmount(0.5f,0.5f))
        .AppendInterval(1f)
        .Append(gameOverTitle.DOFillAmount(1f,0.5f))
        .AppendInterval(1f)
        .Append(continueLabel.DOFade(2f,1))
        .Join(DOVirtual.Float(80,0,2,x => continueLabel.characterSpacing = x))
        .AppendCallback(() => {
            canContinue = true;
        });
    }

    public void Conitnue() {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() => SEManager.Instance.Play(SEPath.DETERMINE))
        .AppendInterval(1f)
        .Append(screenDissolve.DOFloat(1,"_Progress",1).SetEase(Ease.Linear))
        .AppendCallback(() => SceneManager.LoadScene("Game"));
    }
}
