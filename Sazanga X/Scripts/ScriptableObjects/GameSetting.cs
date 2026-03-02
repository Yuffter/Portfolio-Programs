using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameSetting : ScriptableObject
{
    static GameSetting _gameSetting;
    const string FILE_PATH = "ScriptableObjects/GameSetting";
    public static GameSetting Instance
    {
        get
        {
            if (_gameSetting == null)
            {
                _gameSetting = Resources.Load<GameSetting>(FILE_PATH);
            }

            return _gameSetting;
        }
    }

    //移動可能行と列数
    public int movableRow, movableColumn;
    public int oneBlockSize = 1;
}
