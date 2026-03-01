using State;
using TMPro;
using UnityEngine;

public class CheckState : MonoBehaviour
{
    private TMP_Text _text;

     private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.SetText($"CurrentGameStateProp: {GameStateMachine.Instance.CurrentGameStateProp.CurrentValue}\nCurrentStateInstance: {GameStateMachine.Instance.CurrentStateInstance}");
    }
}
