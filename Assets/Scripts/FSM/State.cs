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

    }
    //public abstract void OnEnter(FSM _owner);
    //public abstract void OnUpdate();
    //public abstract void OnExit();
    //public abstract void DebugState();
}
