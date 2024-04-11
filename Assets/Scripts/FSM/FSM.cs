using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    State currentState = null;
    public List<State> AllStates { get; set; } = new List<State>();

    public void StartFSM()
    {
        if(AllStates.Count == 0)
        {
            Debug.LogWarning("Cannot start FSM, no state are present");
            return;
        }
        SetNext(AllStates[0]);
    }

    private void Start() => StartFSM();

    private void Update() => UpdateFSM();

    void UpdateFSM()
    {
        if (currentState == null)
            return;
        currentState.Behaviour?.OnUpdate();
        //currentState?.OnUpdate();
        CheckForValidTransition(currentState);
    }

    void StopFSM()
    {
        currentState?.Behaviour?.OnExit();
        currentState = null;
    }

    private void OnDestroy() => StopFSM();

    public void SetNext(State _newState)
    {
        currentState?.Behaviour?.OnExit();
        currentState = _newState;
        currentState?.Behaviour?.OnEnter(this);
    }

    private void OnDrawGizmos()
    {
        currentState?.Behaviour?.DebugBehaviour();
    }

    public void CheckForValidTransition(State _state)
    {
        if(_state == null)
        {
            Debug.Log("Transition state is null");
            return;
        }    
        for (int i = 0; i < _state.Transitions.Count; i++)
        {
            if (_state.Transitions[i].IsValid())
                //_state.Transitions[i].CallNext();
                Debug.Log("Implement call next transition");
        }
    }

    public void AddState()
    {
        AllStates.Add(new State(this));
    }

    public void AddState(Behaviour _behaviour)
    {
        State _newState = new State(this);
        _behaviour.Owner = this;
        _newState.Behaviour = _behaviour;
        AllStates.Add(_newState);
    }

    public void AddTransition(State _startState, State _endState, Transition _transition)
    {
        Debug.Log("Implement");
    }
}
