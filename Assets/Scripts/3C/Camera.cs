using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Camera))]
public class Camera : MonoBehaviour
{
    [SerializeField, Range(0, 100)] float cameraHeight = 5;
    [SerializeField, Range(.01f, 100)] float cameraDistance = 5;
    [SerializeField, Range(0, 360)] float cameraRotation = 45;
    void Start()
    {
        float radAngle = Mathf.Deg2Rad * cameraRotation;

        transform.position = new Vector3(Mathf.Sin(radAngle), 0, Mathf.Cos(radAngle)) * cameraDistance + new Vector3(0, cameraHeight, 0) + transform.parent.position;
        transform.LookAt(transform.parent);
    }

    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Vector3 _playerPos = transform.parent.position;
        Gizmos.color = Color.blue;

        float radAngle = Mathf.Deg2Rad * cameraRotation;

        Vector3 _camPos = new Vector3(Mathf.Sin(radAngle), 0, Mathf.Cos(radAngle)) * cameraDistance + new Vector3(0, cameraHeight, 0) + transform.parent.position;

        Gizmos.DrawWireCube(_camPos, Vector3.one);
        Gizmos.DrawLine(_camPos, _playerPos);
    }
}
