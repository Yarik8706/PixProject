using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform centerPosition;

    private void LateUpdate()
    {
        transform.position = new Vector3(centerPosition.position.x, centerPosition.position.y, -10);
    }
}