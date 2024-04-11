using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "ScriptableObjects/New Stats", order = 1)]
public class Stats : ScriptableObject
{
    [SerializeField, Range(0,100)]
    public float speed = 2;
}
