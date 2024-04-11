using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "ScriptableObjects/Behaviour/Idle")]
public class BehaviourIdle : Behaviour
{

    public override void DebugBehaviour()
    {
        //Do nothing
    }

    public override void OnEnter(FSM _owner)
    {
        //Do nothing
        Debug.Log("Entered Idle");
    }

    public override void OnExit()
    {
        //Do nothing
    }

    public override void OnUpdate()
    {
        //Do nothing
        Debug.Log("Idling");
    }
}
