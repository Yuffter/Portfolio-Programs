using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtPieceGenerator : MonoBehaviour
{
    [SerializeField] GameObject art;
    [SerializeField] GameObject artEffect;
    [SerializeField] PiecePositionsData piecePositionsData;
    [SerializeField] Vector2 collisionScale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate()
    {
        art.GetComponent<Explodable>().fragmentInEditor();
        var fragments = art.GetComponent<Explodable>().fragments;
        art.GetComponent<Explodable>().explode();

        List<Vector3> randomPositions = new List<Vector3>(piecePositionsData.positions);
        ShufflePositions(ref randomPositions);

        for (int i = 0;i < fragments.Count;i++)
        {
            fragments[i].transform.position = randomPositions[i];
            fragments[i].GetComponent<Rigidbody2D>().gravityScale = 0f;
            Destroy(fragments[i].GetComponent<PolygonCollider2D>());
            fragments[i].AddComponent<BoxCollider2D>();
            fragments[i].GetComponent<BoxCollider2D>().isTrigger = true;
            fragments[i].GetComponent<BoxCollider2D>().size = collisionScale;
            //fragments[i].GetComponent<PolygonCollider2D>().isTrigger = true;

            fragments[i].AddComponent<PieceController>();
            GameObject g = Instantiate(artEffect, fragments[i].transform);
            g.transform.localScale = new Vector3(1f / fragments[i].transform.localScale.x, 1f / fragments[i].transform.localScale.y, 1f / fragments[i].transform.localScale.z);
            fragments[i].transform.parent = GameObject.Find("ArtPieces").transform;
        }
    }

    void ShufflePositions(ref List<Vector3> pos)
    {
        int n = pos.Count;

        while (n > 1)
        {
            n--;

            int rnd = Random.Range(0, n + 1);

            Vector3 tmp = pos[rnd];
            pos[rnd] = pos[n];
            pos[n] = tmp;
        }
    }
}
