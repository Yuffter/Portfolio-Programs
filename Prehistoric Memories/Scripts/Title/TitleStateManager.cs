using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStateManager : MonoBehaviour
{
    public enum ElementState
    {
        START,
        EXIT
    }

    public ElementState currentState {  get; private set; }

    public void ChangeState(int value)
    {
        currentState += value;
    }

    public void ChangeState(ElementState value)
    {
        currentState = value;
    }
}
