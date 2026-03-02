using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu]
public class StagesData : ScriptableObject
{
    static StagesData _stagesData;
    const string FILE_PATH = "Scriptables/StagesData";

    public static StagesData Instance
    {
        get
        {
            if (_stagesData == null)
            {
                _stagesData = Resources.Load(FILE_PATH) as StagesData;
            }

            return _stagesData;
        }
    }

    public int currentStageIndex = 0;
    public List<StageData> stagesData = new List<StageData>();
}

[Serializable]
public class StageData
{
    public bool isCleared = false;
    public bool isOpened = false;
    public bool isChanged = false;
    public Sprite closedArt, openedArt;
    public Vector3 position;
    public Vector3 playerSpawnPosition;
    public int PlayerHp;
}
