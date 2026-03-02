using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using KanKikuchi.AudioManager;

public class TitleContentPresenter : MonoBehaviour
{
    [SerializeField] TitleContentModel titleContentModel;
    [SerializeField] TitleContentView titleContentView;
    // Start is called before the first frame update
    void Start()
    {
        titleContentModel.CurrentContentNumber
            .Skip(1)
            .DistinctUntilChanged()
            .Subscribe(x =>
            {
                titleContentView.OnChangeContent(x);
                SEManager.Instance.Play(SEPath.TITLE_CONTENT_SELECT);
            });

        titleContentModel.IsDecided
            .Skip(1)
            .DistinctUntilChanged()
            .Where(flag => flag == true)
            .Subscribe(x =>
            {
                titleContentView.OnDecision(titleContentModel.contentType);
                SEManager.Instance.Play(SEPath.DETERMINE);
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
