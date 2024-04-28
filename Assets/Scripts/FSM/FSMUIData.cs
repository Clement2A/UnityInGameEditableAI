using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMUIData
{
    public List<Vector2> StatePositions { get; set; } = new List<Vector2>();

    public void AddStatePosition(Vector2 _position)
    {
        StatePositions.Add(_position);
    }

    public void RemoveState(int _stateUiStateIndex)
    {
        StatePositions.RemoveAt(_stateUiStateIndex);
    }
}
