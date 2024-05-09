using UnityEngine;

public abstract class Condition : ScriptableObject
{
    [SerializeField] string conditionName = "No name";

    public string ConditionName { get => conditionName; set => conditionName = value; }
    public FSM Owner { get; set; }
    public abstract void Init();
    public abstract bool IsConditionVerified();
    public abstract void DrawDebug();
}
