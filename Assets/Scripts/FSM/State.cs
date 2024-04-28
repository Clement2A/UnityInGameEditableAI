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

    public void AddTransition(State _nextState)
    {
        Transition _newTransition = new Transition(Owner, _nextState);
        Transitions.Add(_newTransition);
    }
    //public abstract void OnEnter(FSM _owner);
    //public abstract void OnUpdate();
    //public abstract void OnExit();
    //public abstract void DebugState();
}
