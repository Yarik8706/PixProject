using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 10f;

    private void Update()
    {
        Vector3 toTarget = target.position - transform.position;
        float angel = Mathf.Atan2(toTarget.y , toTarget.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.AngleAxis(angel, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
