using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class RemainingPieceModel : MonoBehaviour
{
    public IReadOnlyReactiveProperty<int> PieceCount => pieceCount;
    ReactiveProperty<int> pieceCount = new ReactiveProperty<int>(0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseRemainingPieceCount()
    {
        pieceCount.Value--;
    }

    public void SetRemainingPieceCount()
    {
        List<PieceController> pieces = FindObjectsByType<PieceController>(FindObjectsSortMode.None).ToList();
        var result = pieces.Where(p => !p.isCollected).ToList();
        pieceCount.Value = result.Count;
    }
}
