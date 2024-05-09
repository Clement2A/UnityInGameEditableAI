using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "WithinPlayerDistance", menuName = "ScriptableObjects/Condition/Within Player Distance")]

public class ConditionWithinPlayerDistance : Condition
{
    [SerializeField] float distance = 3;
    public override void Init()
    {
        //Nothing
    }

    public override bool IsConditionVerified()
    {
        
        Vector3 _playerPosition = PlayerSystem.Instance.Player.transform.position;
        float _distSq = Mathf.Pow(_playerPosition.z - Owner.transform.position.z, 2) +
                        Mathf.Pow(_playerPosition.x - Owner.transform.position.x, 2);
        return _distSq < Mathf.Pow(distance, 2);
    }

    public override void DrawDebug()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Owner.transform.position, distance);
    }
}
