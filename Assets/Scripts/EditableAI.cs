using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FSM))]

public class EditableAI : MonoBehaviour
{
    public event Action<EditableAI> OnClick = null;

    public FSM Fsm { get; set; }
    public FSMUIData FsmUiData { get; set; } = new FSMUIData();

    void Start()
    {
        Fsm = GetComponent<FSM>();
        EditableAIManager.Instance.RegisterAI(this);
    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        OnClick?.Invoke(this);
    }

    public void AddState(Vector2 _position)
    {
        Fsm.AddState();
        FsmUiData.AddStatePosition(_position);
    }

    public State AddState(Vector2 _position, Behaviour _behaviour)
    {
        State _newState = Fsm.AddState(_behaviour);
        FsmUiData.AddStatePosition(_position);
        return _newState;
    }

    public void RemoveState(int _stateIndex)
    {
        Fsm.RemoveState(_stateIndex);
        FsmUiData.RemoveState(_stateIndex);
    }
}
