public class Transition
{
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
    }

    public void DrawDebug()
    {
        Condition?.DrawDebug();
    }
}