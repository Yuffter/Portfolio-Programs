using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CreaturesData : ScriptableObject
{
    static CreaturesData _creaturesData;
    const string FILE_PATH = "Scriptables/CreaturesData";

    public static CreaturesData Instance
    {
        get
        {
            if (_creaturesData == null)
            {
                _creaturesData = Resources.Load(FILE_PATH) as CreaturesData;
                _creaturesData.dCreaturesData = new Dictionary<int, CreatureData>();
                foreach (var c in _creaturesData.creaturesData)
                {
                    _creaturesData.dCreaturesData.Add(c.id, c);
                }
            }

            return _creaturesData;
        }
    }

    public List<CreatureData> creaturesData = new List<CreatureData>();
    public Dictionary<int, CreatureData> dCreaturesData = new Dictionary<int, CreatureData>(); 
}

[Serializable]
public class CreatureData
{
    public int id;
    public string name;
    public Sprite sprite;
    public int requiredColorRed;
    public int requiredColorGreen;
    public int requiredColorBlue;
    public GameObject creaturePrefab;
}