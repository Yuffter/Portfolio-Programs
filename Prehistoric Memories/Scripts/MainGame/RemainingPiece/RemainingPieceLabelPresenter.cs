using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class RemainingPieceLabelPresenter : MonoBehaviour
{
    TextMeshProUGUI label;
    [SerializeField] RemainingPieceModel remainingPieceModel;
    private void Awake()
    {
        label = GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {

        remainingPieceModel.PieceCount
            .Subscribe(_ =>
            {
                label.text = $"残りピース数 {remainingPieceModel.PieceCount.Value}";
            });
    }
}
