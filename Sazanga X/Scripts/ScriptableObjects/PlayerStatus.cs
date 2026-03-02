using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStatus : ScriptableObject
{
    static PlayerStatus _playerStatus;
    const string FilePath = "ScriptableObjects/PlayerStatus";
    public static PlayerStatus Instance
    {
        get
        {
            if (_playerStatus == null)
            {
                _playerStatus = Resources.Load<PlayerStatus>(FilePath);
            }

            return _playerStatus;
        }
    }

    public float attack = 10f;
    public float maxHp = 100f;
    public float currentHp = 100f;
    public float actionPoint = 100f;
    public int currentRow, curentColumn;
    public float moveDuration = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
