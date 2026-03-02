using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using KanKikuchi.AudioManager;

public class TNTBlock : MonoBehaviour
{
    [SerializeField] float explosionRadius = 1.5f;
    [SerializeField] GameObject explosionParticle;
    [SerializeField] Vector2 explosionForce;

    [Header("指定したオブジェクトを破壊する場合")]
    [SerializeField] List<GameObject> destroyObject = new List<GameObject>();

    NewCreatureController newCreatureController;
    Material selfMaterial;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<NewCreatureController>(out newCreatureController);
        selfMaterial = GetComponent<SpriteRenderer>().material;

        this.UpdateAsObservable()
            .Where(_ => selfMaterial.GetFloat("_Threshold") == 1)
            .First()
            .Subscribe(_ => StartCoroutine(Explode())).AddTo(this);

        this.UpdateAsObservable()
            .Where(_ => newCreatureController != null)
            .Where(_ => newCreatureController.isMaterialize)
            .First()
            .Subscribe(_ => StartCoroutine(Explode())).AddTo(this);
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(1f);

        if (destroyObject.Count == 0)
        {
            print("Called");
            GameObject[] g = GameObject.FindGameObjectsWithTag("DestroyObject");
            for (int i = 0; i < g.Length; i++)
            {
                if (Vector3.Distance(transform.position, g[i].transform.position) <= explosionRadius)
                {
                    destroyObject.Add(g[i]);
                }
            }
        }

        Instantiate(explosionParticle, transform.position, Quaternion.identity);

        for (int i = 0; i < destroyObject.Count; i++)
        {
            Instantiate(explosionParticle, destroyObject[i].transform.position, Quaternion.identity);
            var exp = destroyObject[i].AddComponent<Explodable>();
            exp.extraPoints = 10;
            exp.orderInLayer = 11;
            exp.fragmentInEditor();
            var frag = exp.fragments;
            exp.explode();
            foreach (var f in frag)
            {
                f.GetComponent<PolygonCollider2D>().isTrigger = true;
                f.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1000, 1000), Random.Range(-500, 2000)));
            }
        }

        GameObject player = GameObject.Find("Player");

        Vector2 dir = (player.transform.position - transform.position).normalized;
        float distance = Mathf.Min(explosionRadius, Vector3.Distance(player.transform.position, transform.position));
        //print($"{dir} {distance}");
        player.GetComponent<Rigidbody2D>().AddForce(dir * explosionForce * (explosionRadius - distance));
        SEManager.Instance.Play(SEPath.EXPLOSION);

        Destroy(gameObject);
    }
}
