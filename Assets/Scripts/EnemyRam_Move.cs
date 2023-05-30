using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRam_Move : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private LayerMask blockingLayer;
    private int curectPoint = 0;

    private void Update()
    {
        CheckPlayerReach();
        MovingRamOnPOints();
        Debug.DrawLine(transform.position, playerTransform.position);
    }


    private bool CheckPlayerReach()
    {        
        RaycastHit2D hit = Physics2D.Linecast(playerTransform.position, transform.position, blockingLayer);


        if (hit.collider != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    private void MovingRamOnPOints()
    {
        if (curectPoint < points.Length)// перемещение ram по точкам в пространстве
        {
            Transform curectOnePoint = points[curectPoint];

            Vector3 direction = (curectOnePoint.position - transform.position).normalized;

            float angel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;// слежение за точкой куда он летит

            transform.rotation = Quaternion.AngleAxis(angel, Vector3.forward);

            Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;

            transform.position = newPosition;

            if (Vector3.Distance(transform.position, curectOnePoint.position) < 0.1f)
            {
                curectPoint = RandomPoints();
                if (curectPoint >= points.Length)
                {
                    curectPoint = 0;
                }
            }
            if (CheckPlayerReach())
            {
                moveSpeed = 0;
                Vector3 playerDistance = (playerTransform.position - transform.position).normalized;
                float playerAngel = Mathf.Atan2(playerDistance.y, playerDistance.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(playerAngel, Vector3.forward);

            }
            else
            {
                moveSpeed = 5f;
            }
        }    
    }

    private int RandomPoints()
    {
        System.Random random = new System.Random();
        return random.Next(0, points.Length);
    }
    private void FindPlayer()
    {

    }
}