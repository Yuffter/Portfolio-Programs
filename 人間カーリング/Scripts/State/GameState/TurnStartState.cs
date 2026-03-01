using System;
using Animation;
using Cysharp.Threading.Tasks;
using SO;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using AudioManager.SE;

namespace State
{
    public class TurnStartState : StateBase
    {
        private TurnTextAnimator _turnTextAnimator;
        private TurnItemSpawnDataBase _turnItemSpawnDataBase; // ターンごとのアイテム出現データベースの参照SO

        public override async void OnEnter()
        {
            var currentStageData = ServiceLocator.Resolve<SO.CurrentStageData>();
            _turnTextAnimator = ServiceLocator.Resolve<Animation.TurnTextAnimator>();
            _turnItemSpawnDataBase = ServiceLocator.Resolve<TurnItemSpawnDataBase>();

            if (currentStageData.Data.CurrentTurnLeft == 0)
            {
                GameStateMachine.Instance.ChangeState(GameState.GameEnd);
                return;
            }
            await UniTask.Delay(TimeSpan.FromSeconds(1));

            //ここにアイテム生成処理を記述
            //アイテム出現データ取得
            var turnItemSpawnData = _turnItemSpawnDataBase.GetTurnItemSpawnData(currentStageData.Data.MaxTurnCount - currentStageData.Data.CurrentTurnLeft);

            Debug.Log("現在のターン数: " + (currentStageData.Data.MaxTurnCount - currentStageData.Data.CurrentTurnLeft));

            Debug.Log("Turn Item Spawn Data取得: " + (turnItemSpawnData != null ? "成功" : "失敗"));
            if (turnItemSpawnData != null)
            {
                var spawnList = new List<ItemType>();
                //出現させるアイテムリストを作成
                foreach (var itemSpawnSetting in turnItemSpawnData.ItemSpawnSettings)
                {
                    for (int i = 0; i < itemSpawnSetting.Count; i++)
                    {
                        //出現させるアイテムをリストに追加 A=>B=>C アイテムのように連結
                        spawnList.Add(itemSpawnSetting.ItemType);
                    }
                }

                for (int i = 0; i < spawnList.Count; i++)
                {
                    int randomIndex = UnityEngine.Random.Range(i, spawnList.Count); // ランダムなインデックスを取得
                    (spawnList[i], spawnList[randomIndex]) = (spawnList[randomIndex], spawnList[i]); // アイテムListシャッフル
                }

                foreach (var itemType in spawnList)
                {
                    //アイテム生成、最大可能性生成数(spawn可能位置がない場合)を超える場合はアイテム生成されない
                    //Debug.Log(Time.time + " アイテム生成: " + itemType);
                    ServiceLocator.Resolve<ItemFacade>().CreateItem(itemType);
                }
            }

            // エフェクト系と各プレイヤーのステータスをリセット
            foreach (var player in ServiceLocator.Resolve<RunTimePlayerBaseSO>().GetAllResources())
            {
                player.ResetAttack();
                player.ResetPointRate();
                player.ResetScoreRate();
                player.ResetSpeed();

                // プレイヤーについているItemEffectBaseコンポーネントを全て取得して削除
                var itemEffects = player.GetComponents<InGame.ItemEffect.ItemEffectBase>();
                foreach (var effect in itemEffects)
                {
                    GameObject.Destroy(effect);
                }
            }

            _turnTextAnimator.Show(currentStageData.Data.CurrentTurnLeft).Forget();
            SEManager.Instance.Play(SEName.TurnChange);
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            await _turnTextAnimator.Hide();
            GameStateMachine.Instance.ChangeState(GameState.Controllable);
            Debug.Log("TurnStartState OnEnter");
        }

        public override void OnExit()
        {
            var currentStageData = ServiceLocator.Resolve<SO.CurrentStageData>();
            currentStageData.Data.CurrentTurnLeft--;
            Debug.Log($"残りターン数: {currentStageData.Data.CurrentTurnLeft}");
        }
    }
}
