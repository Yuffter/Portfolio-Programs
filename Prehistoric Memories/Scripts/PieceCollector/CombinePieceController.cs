using DG.Tweening;
using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CombinePieceController : MonoBehaviour
{
    public int pieceIndex = -1;
    bool isBeingClicked = false;
    bool isCorrent = false;
    PieceCollectorManager pieceCollectorManager;
    Rigidbody2D myRigid;

    float rotationSpeed = 180;

    // Start is called before the first frame update
    void Start()
    {
        pieceCollectorManager = FindAnyObjectByType<PieceCollectorManager>();
        myRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigid.freezeRotation = false;
        if (!isBeingClicked) return;

        myRigid.freezeRotation = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;

        //âEâÒì]
        if (Input.GetKey(KeyCode.Z))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        }

        //ç∂âÒì]
        if (Input.GetKey(KeyCode.X))
        {
            transform.Rotate(Vector3.back * Time.deltaTime * rotationSpeed);
        }
    }

    public void OnMouseDown()
    {
        if (!pieceCollectorManager.canCombine || isCorrent) return;
        isBeingClicked = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    public void OnMouseUp()
    {
        isBeingClicked = false;
        GetComponent<Rigidbody2D>().isKinematic = false;
        CheckIfPieceFit();
    }

    void CheckIfPieceFit()
    {
        if (isCorrent) return;

        float distance = Vector3.Distance(transform.position, pieceCollectorManager.backgroundArtPieces[pieceIndex].transform.position);
        float angleDiff = Mathf.Min(Mathf.Abs(transform.eulerAngles.z), 360 - Mathf.Abs(transform.eulerAngles.z));


        if (distance <= 0.3f && angleDiff <= 20f)
        {
            isCorrent = true;
            GetComponent<MeshRenderer>().sortingOrder = 1;
            pieceCollectorManager.FitPiece();
            StartCoroutine(PlayFittingAnimation());
        }
    }

    IEnumerator PlayFittingAnimation()
    {
        SEManager.Instance.Play(SEPath.CORRECT);
        Destroy(GetComponent<Rigidbody2D>());
        transform.position = pieceCollectorManager.backgroundArtPieces[pieceIndex].transform.position;
        transform.eulerAngles = Vector3.zero;

        yield return new WaitForSeconds(1f);

        GetComponent<MeshRenderer>().materials[1].DOFloat(0f, "_Threshold", 1);
    }
}
