using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class AbilitiesData : ScriptableObject
{
    static AbilitiesData _abilitiesData;
    const string FILE_PATH = "Scriptables/AbilitiesData";
    public static AbilitiesData Instance
    {
        get
        {
            if (_abilitiesData == null)
            {
                _abilitiesData = Resources.Load(FILE_PATH) as AbilitiesData;
            }

            return _abilitiesData;
        }
    }
    public List<AbilityData> abilities = new List<AbilityData>();
}

[Serializable]
public class AbilityData
{
    public string abilityName;
    public bool isEnable;
    [Multiline(5)]
    public string abilityDescription;
}