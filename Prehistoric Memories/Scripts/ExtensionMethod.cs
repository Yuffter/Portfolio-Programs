using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Tilemaps;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Reflection;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public static class ExtensionMethodList
{
    //float型が小数かどうか 整数:false 小数:true
    public static bool IsExistAfterDecimalPoint(this float self)
    {
        return self % 1 != 0;
    }

    //x,y,xyをセット
    public static void SetX(this Transform transform, float x)
    {
        transform.position = new Vector3(x, transform.position.y);
    }
    public static void SetY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y);
    }
    public static void SetXY(this Transform transform, float x, float y)
    {
        transform.position = new Vector3(x, y);
    }
    //スプライトセット
    public static void SetSprite(this GameObject gameObject, Sprite sprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public static void Hide(this GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
    public static void Show(this GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    public static void SetColor(this GameObject gameObject, float r, float g, float b, float a)
    {
        if (gameObject.GetComponent<UnityEngine.UI.Image>() == null) gameObject.GetComponent<SpriteRenderer>().color = new Color(r, g, b, a);
        else gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(r, g, b, a);
    }
    
    /// <summary>
    /// 与えられたオブジェクトの子から特定の名前のオブジェクトを探す
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="objName"></param>
    /// <returns></returns>
    public static GameObject SearchChild(this GameObject parent,string objName)
    {
        Queue<GameObject> que = new Queue<GameObject>();
        que.Enqueue(parent);

        while (que.Count != 0)
        {
            GameObject g = que.Dequeue();

            if (g.name == objName) return g;
            foreach (Transform child in g.transform)
            {
                que.Enqueue(child.gameObject);
            }
        }

        return null;
    }

    /// <summary>
    /// SortingLayerを設定する
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="number"></param>
    public static void SetLayerNumber(this GameObject gameObject,int number)
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = number;
    }

    /// <summary>
    /// オブジェクトの初期化
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    /// <param name="scale"></param>
    public static void Init(this GameObject gameObject,Vector3 position,Vector3 angle,Vector3 scale)
    {
        gameObject.transform.position = position;
        gameObject.transform.eulerAngles = angle;
        gameObject.transform.localScale = scale;
    }

    /// <summary>
    /// マテリアルを設定する
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="material"></param>
    public static void SetMaterial(this GameObject gameObject,Material material)
    {
        if (gameObject.GetComponent<SpriteRenderer>())
        {
            gameObject.GetComponent<SpriteRenderer>().material = material;
        }
        if (gameObject.GetComponent<MeshRenderer>())
        {
            gameObject.GetComponent<MeshRenderer>().material = material;
        }
    }

    public static void ComeAsShock(float fadeTime)
    {
        Color originalColor = Camera.main.backgroundColor;
        Camera.main.DOColor(Color.white, 0.1f).OnComplete(
            () =>
            {
                DOVirtual.DelayedCall(0.5f, () =>
                {
                    Camera.main.DOColor(originalColor, fadeTime);
                });
            });
        foreach (SpriteRenderer sprite in UnityEngine.Object.FindObjectsOfType(typeof(SpriteRenderer)))
        {
            Color col = sprite.color;
            sprite.DOColor(Color.black, 0.1f).OnComplete(
                () =>
                {
                    DOVirtual.DelayedCall(1, () =>
                    {
                        sprite.DOColor(col, fadeTime);
                    });

                });
        }
        foreach (UnityEngine.UI.Text text in UnityEngine.Object.FindObjectsOfType(typeof(UnityEngine.UI.Text)))
        {
            Color col = text.color;
            text.DOColor(Color.black, 0.1f).OnComplete(
                () =>
                {
                    DOVirtual.DelayedCall(1, () =>
                    {
                        text.DOColor(col, fadeTime);
                    });
                });
        }
        foreach (Tilemap tilemap in UnityEngine.Object.FindObjectsOfType(typeof(Tilemap)))
        {
            Color col = tilemap.color;
            DOVirtual.Color(col, Color.black, 0.1f, x => tilemap.color = x).OnComplete(
                () =>
                {
                    DOVirtual.DelayedCall(1, () =>
                    {
                        DOVirtual.Color(Color.black, col, fadeTime, x => tilemap.color = x);
                    });
                });
        }
        foreach (UnityEngine.UI.Image image in UnityEngine.Object.FindObjectsOfType(typeof(UnityEngine.UI.Image)))
        {
            if (image.color.a == 0) continue;
            if (image.name == "PostEffect") continue;
            Color col = image.color;
            image.DOColor(Color.black, 0.1f).OnComplete(
                () =>
                {
                    DOVirtual.DelayedCall(1, () =>
                    {
                        image.DOColor(col, fadeTime);
                    });
                });
        }
    }
}

public class ExtensionMethod : MonoBehaviour
{
    
}
