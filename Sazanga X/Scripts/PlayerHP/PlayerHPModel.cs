using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using KanKikuchi.AudioManager;

public class PlayerHPModel : MonoBehaviour
{
    readonly ReactiveProperty<float> _playerHP = new ReactiveProperty<float>();
    public IReadOnlyReactiveProperty<float> PlayerHP => _playerHP;
    private void Awake()
    {
        _playerHP.Value = PlayerStatus.Instance.maxHp;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Z))
        {
            _playerHP.Value = Mathf.Max(0, _playerHP.Value - 10);
        }*/
    }

    private void OnDestroy()
    {
        _playerHP.Dispose();
    }

    public void Damage(float amount) {
        _playerHP.Value = Mathf.Max(0, _playerHP.Value - amount);
        SEManager.Instance.Play(SEPath.DAMAGE_1);
    }
}
