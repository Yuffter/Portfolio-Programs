using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;
using KanKikuchi.AudioManager;
using AnnulusGames.LucidTools.Inspector;
using UniRx;
using UniRx.Triggers;

[RequireComponent(typeof(RequireColorShower))]
[RequireComponent(typeof(NewCreatureCreateController))]
public class ObjectOutline : MonoBehaviour
{
    public int id;
    public float lineWidth;
    public int sortingOrder = -1;
    [Header("線のマテリアル")]
    public Material mat;
    List<Vector3> vertices = new List<Vector3>();
    LineRenderer line;

    public Vector2 perOneTileSize;

    [Header("必要なインク量(Red,Green,Blueの順)")]
    public List<float> requireInk = new List<float>();

    public IReadOnlyReactiveProperty<bool> IsMaterialized => isMaterialized;
    ReactiveProperty<bool> isMaterialized = new ReactiveProperty<bool>(false);
    public string tag = null;
    public bool maintainingTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<BoxCollider2D>() != null)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            GetComponent<PolygonCollider2D>().isTrigger = true;
        }
        GetVertices();

        line = gameObject.AddComponent<LineRenderer>();

        StartDrawing();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DrawOutline()
    {
        vertices.Add(vertices[0]);
        line.positionCount= vertices.Count;
        line.SetPositions(vertices.ToArray());
        
    }

    private void GetVertices()
    {
        if (GetComponent<BoxCollider2D>())
        {
            Vector2 offset = GetComponent<BoxCollider2D>().offset * transform.localScale;
            Vector2 size = GetComponent<BoxCollider2D>().size * transform.localScale;
            if (GetComponent<SpriteRenderer>().drawMode == SpriteDrawMode.Tiled)
            {
                size = GetComponent<BoxCollider2D>().size * (GetComponent<SpriteRenderer>().size / perOneTileSize);
            }

            size /= new Vector2(2, 2);

            //右上
            vertices.Add(transform.position + (Vector3)offset + new Vector3(size.x, size.y, 0));
            //右下
            vertices.Add(transform.position + (Vector3)offset + new Vector3(size.x, -size.y, 0));
            //左下
            vertices.Add(transform.position + (Vector3)offset + new Vector3(-size.x, -size.y, 0));
            //左上
            vertices.Add(transform.position + (Vector3)offset + new Vector3(-size.x, size.y, 0));
        }
        else if (GetComponent<PolygonCollider2D>())
        {
            var v = GetComponent<PolygonCollider2D>().points;
            Vector2 offset = GetComponent<PolygonCollider2D>().offset * transform.localScale;
            
            for (int i = 0;i < v.Length;i++)
            {
                vertices.Add(transform.position + (Vector3)offset + Vector3.Scale((Vector3)v[i], transform.localScale));
            }
        }
        else
        {
            Debug.Log("No Collider was attached");
        }
    }

    public void StartDrawing()
    {
        line.SetWidth(lineWidth, lineWidth);
        line.numCornerVertices = 90;
        line.numCapVertices = 90;
        line.material = mat;
        line.sortingOrder = sortingOrder;

        //色ごとの割合を計算する
        float sum = requireInk.Sum();
        line.material.SetColor("_Color", new Color(requireInk[0] / sum, requireInk[1] / sum, requireInk[2] / sum));

        DrawOutline();
    }

    public void Materialize()
    {
        isMaterialized.Value = true;
    }
}
