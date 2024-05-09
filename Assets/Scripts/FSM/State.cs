using System;
using System.Collections.Generic;

public class State
{
    FSM Owner { get; }
    public State(FSM _owner)
    {
        Owner = _owner;
    }
    public Behaviour Behaviour { get; set; }
    public List<Transition> Transitions { get; } = new List<Transition>();

    public void AddTransition(State _nextState, Condition _condition)
    {
        Transition _newTransition = new Transition(Owner, _nextState);
        _condition.Owner = Owner;
        _condition.Init();
        _newTransition.Condition = _condition;
        Transitions.Add(_newTransition);
    }

    public void RemoveTransition(int _index)
    {
        Transitions.RemoveAt(_index);
    }

    public void OnEnter(FSM _fsm)
    {
        Behaviour.OnEnter(_fsm);
        InitTransitions();
    }

    void InitTransitions()
    {
        foreach (Transition _transition in Transitions)
        {
            _transition.Condition.Init();
        }
    }

    public void DrawDebug()
    {
        Behaviour?.DebugBehaviour();
        foreach (Transition _transition in Transitions)
        {
            _transition.DrawDebug();
        }
    }
}
