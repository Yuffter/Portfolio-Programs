using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ArtCheckerController : MonoBehaviour
{
    public IReadOnlyReactiveProperty<bool> IsClear => isClear;
    ReactiveProperty<bool> isClear = new ReactiveProperty<bool>(false);
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < StagesData.Instance.stagesData.Count; i++)
        {
            //ステージクリア直後のものが存在する場合
            if (StagesData.Instance.stagesData[i].isChanged)
            {
                isClear.Value = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
