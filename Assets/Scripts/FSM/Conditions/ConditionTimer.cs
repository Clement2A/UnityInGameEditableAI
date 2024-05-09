using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "TimerCondition", menuName = "ScriptableObjects/Condition/Timer")]

public class ConditionTimer : Condition
{
    [SerializeField] float timer = 5;
    float currentTimer = 0;
    public override void Init()
    {
        currentTimer = timer;
    }

    public override bool IsConditionVerified()
    {
        currentTimer -= Time.deltaTime;
        return currentTimer <= 0;
    }

    public override void DrawDebug()
    {
        Handles.Label(Owner.transform.position, "Time left: + " + currentTimer);
    }
}
