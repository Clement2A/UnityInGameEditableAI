using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatsComponent))]
public class Player : MonoBehaviour
{
    Stats stats;
    Transform cameraTransform = null;
    private void Awake()
    {
        PlayerSystem.Instance.SetPlayer(this);
        stats = GetComponent<StatsComponent>().Stats;
    }
    void Start()
    {
        cameraTransform = transform.GetChild(0);
    }

    void Update()
    {
        Vector3 _camForward = cameraTransform.forward;
        _camForward.y = 0;
        _camForward.Normalize();
        if (Input.GetKey(KeyCode.Z))
            transform.position += _camForward * stats.speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            transform.position += -_camForward * stats.speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Q))
            transform.position += -cameraTransform.right * stats.speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            transform.position += cameraTransform.right * stats.speed * Time.deltaTime;
    }
}
