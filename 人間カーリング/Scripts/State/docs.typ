#import "@preview/js:0.1.3": *
 #show: js.with(
   lang: "ja",
   seriffont-cjk: "Harano Aji Mincho",
   sansfont-cjk: "Harano Aji Gothic"
 )

// コードブロックの背景を白にする
#show raw: set block(fill: white, stroke: 0.5pt)

// ページ番号の設定
#set page(numbering: "1")
// すべての heading 要素の表示方法を定義する
#show heading: set text(weight: "bold",font: "Harano Aji Gothic")
// 本文を明朝体に設定、フォントサイズを10.5ptに設定
#set text(
  size: 10.5pt, 
  font: "Harano Aji Mincho",
  lang: "ja",
  )

// ページ番号の設定
#set page(numbering: "1")

#show "、": "，" // 読点をカンマに置き換える
#show "。": "．" // 句点をピリオドに置き換える

// 目次の表示
// #outline()
// #pagebreak()

// ここから本文
= State Machineの使い方

== 概要
このステートマシンは，Unityのシーン上で`MonoBehaviour`を継承したコンポーネントとして各状態を管理する設計になっています．

- `GameStateMachine`がシングルトンとして全体の管理者となり，状態遷移の受付と実行を担当します．
- 各状態は`StateBase`を継承した`MonoBehaviour`クラスとして実装し，GameObjectにアタッチして利用します．
- 状態遷移は`GameState`というenumを用いて型安全に行われます．

== 使い方
=== シーンのセットアップ
1. 空のGameObjectを作成し，`GameStateMachine.cs`をアタッチします．
2. インスペクターで`Initial Game State`に初期状態（例：`Title`）を設定します．

=== 新しいステートの作成
1. `StateBase`を継承した新しいC\#スクリプトを作成します．（例：`MyNewState.cs`）
2. `OnEnter`, `OnExit`, `OnUpdate`をオーバーライドして，そのステートの処理を実装します．
3. `Awake`内で`AddTransition`を呼び出し，遷移可能なステートを登録します．
4. シーン内に空のGameObjectを作成し，作成したステートスクリプト（`MyNewState.cs`）をアタッチします．
5. インスペクターで，そのステートに対応する`GameState`（例：`MyNewState`）を設定します．

=== 3. ステートの遷移
状態遷移を行いたいタイミングで，`GameStateMachine`の`ChangeState`メソッドを呼び出します．
```csharp
// 例：Zキーが押されたらInGameステートに遷移する
if (Input.GetKeyDown(KeyCode.Z))
{
    GameStateMachine.Instance.ChangeState(GameState.InGame);
}
```

== コード例
以下は`TitleState`と`InGameState`の実装例です．

=== `TitleState.cs`
```csharp
using TNRD;
using UnityEngine;

public class TitleState : StateBase
{
    protected override void Awake()
    {
        base.Awake();
        // InGameステートへの遷移を許可する
        AddTransition<InGameState>(GameState.InGame);
    }

    public override void OnEnter()
    {
        Debug.Log("TitleStateが開始されました");
    }

    public override void OnExit()
    {
        Debug.Log("TitleStateが終了しました");
    }

    public override void OnUpdate()
    {
        // ZキーでInGameステートへ遷移
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameStateMachine.Instance.ChangeState(GameState.InGame);
        }
    }
}
```

=== `InGameState.cs`
```csharp
using TNRD;
using UnityEngine;

public class InGameState : StateBase
{
    public override void OnEnter()
    {
        Debug.Log("InGameStateが開始されました");
    }

    public override void OnExit()
    {
        Debug.Log("InGameStateが終了しました");
    }

    public override void OnUpdate()
    {
        // AキーでTitleステートへ遷移
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameStateMachine.Instance.ChangeState(GameState.Title);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        // Titleステートへの遷移を許可する
        AddTransition<TitleState>(GameState.Title);
    }
}
```