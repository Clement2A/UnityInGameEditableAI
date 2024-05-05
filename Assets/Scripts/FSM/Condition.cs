using UnityEngine;

public abstract class Condition : ScriptableObject
{
    string conditionName = "No name";

    public string ConditionName { get => conditionName; set => conditionName = value; }
    public abstract bool IsConditionVerified();
}
