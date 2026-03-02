using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PieceAnimationController))]
[RequireComponent(typeof(PieceCollider))]
public class PieceController : MonoBehaviour
{
    PieceAnimationController pieceAnimationController;
    PieceCollider pieceCollider;
    public bool isCollected { get; private set; } = false;
    private void Awake()
    {
        pieceAnimationController = GetComponent<PieceAnimationController>();
        pieceCollider = GetComponent<PieceCollider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollideWithPlayer()
    {
        isCollected = true;
        pieceAnimationController.Collect();
        FindAnyObjectByType<RemainingPieceModel>().DecreaseRemainingPieceCount();
    }
}
