using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behaviour : ScriptableObject
{
    [SerializeField]
    string stateName = "No name";

    public string StateName { get => stateName; set => stateName = value; }

    public FSM Owner { get; set; }

    public abstract void OnEnter(FSM _owner);
    public abstract void OnUpdate();
    public abstract void OnExit();
    public abstract void DebugBehaviour();
}
