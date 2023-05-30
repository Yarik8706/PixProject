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
        MoveOnPoint();
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


    private void MoveOnPoint()
    {
        if (curectPoint < points.Length)
        {
            Transform curectOnePoint = points[curectPoint];

            Vector3 direction;

            if (CheckPlayerReach())
            {
                direction = (playerTransform.position - transform.position).normalized;
            }

            else
            {
                direction = (curectOnePoint.position - transform.position).normalized;
            }

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 newPositon= transform.position + direction * moveSpeed * Time.deltaTime;
            transform.position = newPositon;
            if (CheckPlayerReach())
            {
                float distanseToPoints = Vector3.Distance(transform.position, playerTransform.position);
                if (distanseToPoints <= 0.1f)
                {

                }
            }
            else
            {
                if(Vector3.Distance(transform.position, curectOnePoint.position)< 0.1f)
                {
                    curectPoint = RandomPoints();
                    if(curectPoint >= points.Length)
                    {
                        curectPoint = 0;
                    }
                }
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