#import "@preview/chronos:0.2.1"
#import "@preview/js:0.1.3": *
 #show: js.with(
   lang: "ja",
   seriffont-cjk: "Harano Aji Mincho",
   sansfont-cjk: "Harano Aji Gothic"
 )
#import "@preview/fletcher:0.5.8" as fletcher: diagram, node, edge
#import fletcher.shapes: house, hexagon

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

= ステートの遷移フロー
#let blob(pos, label, tint: white, ..args) = node(
	pos, align(center, label),
	width: 35mm,
	fill: tint.lighten(60%),
	stroke: 1pt + tint.darken(20%),
	corner-radius: 5pt,
	..args,
)

#diagram(
	spacing: 10pt,
	cell-size: (8mm, 10mm),
	edge-stroke: 1pt,
	edge-corner-radius: 5pt,
	mark-scale: 70%,

	// blob((0,1), [Add & Norm], tint: yellow, shape: hexagon),
	// edge(),
	// blob((0,2), [Multi-Head\ Attention], tint: orange),
	// blob((0,4), [Input], shape: house.with(angle: 30deg),
	// 	width: auto, tint: red),

	// for x in (-.3, -.1, +.1, +.3) {
	// 	edge((0,2.8), (x,2.8), (x,2), "-|>")
	// },
	// edge((0,2.8), (0,4)),

	edge((0,3), "r,d,d,d,d,l", "-|>"),

  blob((0,1), [TitleState], tint: red),
  edge("-|>"),
  blob((0,2), [StageSelectState], tint: blue),
  edge("-|>"),
  blob((0,3), [GameStartState], tint: purple),
  edge("-|>"),
  blob((0,4), [TurnStartState], tint: purple),
  edge("-|>"),
  blob((0,5), [ControllableState], tint: purple),
  edge("-|>"),
  blob((0,6), [WaitingState], tint: purple),
	blob((0,7), [GameEndState], tint: purple),
	edge("-|>"),
	blob((0,8), [ResultState], tint: purple)
)