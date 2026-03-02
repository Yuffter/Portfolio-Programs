using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using KanKikuchi.AudioManager;

public class PlayerHPPresenter : MonoBehaviour
{
    [SerializeField] PlayerHPModel playerHPModel;
    [SerializeField] PlayerHPBarView playerHPBarView;
    [SerializeField] PlayerHPEffectView playerHPEffectView;

    [SerializeField] GameEvent gameOverEvent;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerShooting playerShooting;
    [SerializeField] PlayerRigid playerRigid;
    [SerializeField] PlayerAnimation playerAnimation;
    // Start is called before the first frame update
    void Start()
    {
        playerHPModel.PlayerHP
            .Skip(1)
            .Subscribe(x =>
            {
                float ratio = x / PlayerStatus.Instance.maxHp;
                playerHPBarView.UpdateBar(ratio);
            });

        playerHPModel.PlayerHP
            .Where(x => 1 <= x && x <= 10)
            .First()
            .Subscribe(_ =>
            {
                playerHPEffectView.Warn();
            });

        playerHPModel.PlayerHP
        .Where(x => x <= 0)
        .First()
        .Subscribe(_ => {
            gameOverEvent.Raise();
            SEManager.Instance.Play(SEPath.GAME_OVER,1,1.5f);
            // playerMovement.UnableToMove();
            // playerShooting.UnableToShoot();
            // playerHPEffectView.CancelWarn();
            // playerHPEffectView.DeadScreen();
            // playerRigid.Crash();
            // playerAnimation.PlayDeadAnimation();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
