using System;

public class Transition
{
    public event Action<Transition> OnDeletion = null;
    protected FSM owner = null;
    protected State nextState = null;
    public Condition Condition { get; set; }
    public State NextState => nextState;
    public bool IsValid()
    {
        return Condition.IsConditionVerified();
    }
    public Transition(FSM _owner, State _nextState)
    {
        owner = _owner;
        nextState = _nextState;
        _nextState.OnDeletion += DeleteSelf;
    }

    void DeleteSelf()
    {
        OnDeletion?.Invoke(this);
        owner = null;
        nextState = null;
        Condition = null;
    }

    public void DrawDebug()
    {
        Condition?.DrawDebug();
    }
}