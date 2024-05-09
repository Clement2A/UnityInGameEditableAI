using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "OutsidePlayerDistance", menuName = "ScriptableObjects/Condition/Outside Player Distance")]

public class ConditionOutsidePlayerDistance : Condition
{
    [SerializeField] float distance = 3;
    public override void Init()
    {
        //nothing
    }

    public override bool IsConditionVerified()
    {
        Vector3 _playerPosition = PlayerSystem.Instance.Player.transform.position;
        float _distSq = Mathf.Pow(_playerPosition.z + Owner.transform.position.z, 2) +
                        Mathf.Pow(_playerPosition.y + Owner.transform.position.y, 2);
        return _distSq > Mathf.Pow(distance, 2);
    }

    public override void DrawDebug()
    {
        Vector3 _playerPosition = PlayerSystem.Instance.Player.transform.position;
        float _dist = Mathf.Sqrt(Mathf.Pow(_playerPosition.z + Owner.transform.position.z, 2) +
                      Mathf.Pow(_playerPosition.y + Owner.transform.position.y, 2));
        Handles.Label(Owner.transform.position, "Target dist is " + distance + "\nWe're at " + _dist);
    }
}
