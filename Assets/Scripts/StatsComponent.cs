using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsComponent : MonoBehaviour
{
    [SerializeField]
    Stats stats;

    public Stats Stats => stats;
}
