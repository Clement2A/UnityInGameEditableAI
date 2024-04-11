using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FollowState", menuName = "ScriptableObjects/Behaviour/FollowPlayer")]
public class BehaviourFollowPlayer : Behaviour
{
    [SerializeField]
    float distanceSquaredFromPlayer = 2;

    Stats aiStats;
    Transform playerTransform;


    public override void DebugBehaviour()
    {
        Gizmos.color = Color.white;
        Vector3 _playerPos = PlayerSystem.Instance.Player.transform.position;
        Gizmos.DrawWireSphere(_playerPos, Mathf.Sqrt(distanceSquaredFromPlayer));
        Gizmos.DrawLine(Owner.transform.position, _playerPos);
    }

    public override void OnEnter(FSM _owner)
    {
        Debug.Log(Owner);
        aiStats = Owner.GetComponent<StatsComponent>().Stats;
        playerTransform = PlayerSystem.Instance.Player.transform;
        Debug.Log("Entered follow");
    }

    public override void OnExit()
    {
        //Do nothing
    }

    public override void OnUpdate()
    {
        Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, playerTransform.position, aiStats.speed * Time.deltaTime);
        Debug.Log("Following");
    }
}
