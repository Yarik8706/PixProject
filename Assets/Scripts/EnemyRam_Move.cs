using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRam_Move : MonoBehaviour
{
    public Transform[] points;
    public Transform targetPlayer;
    public LineRenderer lineRenderer;
    public float moveRamSpeed = 5f;
    private int curectPoint = 0;

    private void Start()
    {
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        CheackRenderLine();
        MovingRamOnPOints();
        Debug.DrawLine(transform.position, targetPlayer.position); // проводит линию между Ram и игроком
    }


    private void CheackRenderLine()//проверка видет ли противник , игрока , проверка производитья через  Line render 
    {
        Vector2 ramPosition = transform.position;
        Vector2 playerPosition = targetPlayer.position;

        int layerMask = ~(1 << gameObject.layer); // Игнорировать слой объекта EnemyRam_Move

        RaycastHit2D hit = Physics2D.Raycast(ramPosition, playerPosition - ramPosition, Vector2.Distance(ramPosition, playerPosition), layerMask);


        if (hit.collider != null && hit.collider.gameObject == targetPlayer.gameObject)
        {
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
        }
        else
        {
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
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

            Vector3 newPosition = transform.position + direction * moveRamSpeed * Time.deltaTime;

            transform.position = newPosition;

            if (Vector3.Distance(transform.position, curectOnePoint.position) < 0.1f)
            {
                curectPoint = RandomPoints();
                if (curectPoint >= points.Length)
                {
                    curectPoint = 0;
                }
            }
            if (CheckRenderedHit())
            {
                moveRamSpeed = 0;
                Vector3 playerDistance = (targetPlayer.position - transform.position).normalized;
                float playerAngel = Mathf.Atan2(playerDistance.y, playerDistance.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(playerAngel, Vector3.forward);

            }
            else
            {
                moveRamSpeed = 5f;
            }
        }    
    }
    private bool CheckRenderedHit()//проверка на обнаружение игрока 
    {
        Vector2 ramPosition = transform.position;
        Vector2 playerPosition = targetPlayer.position;

        int layerMask = ~(1 << gameObject.layer);

        RaycastHit2D hit = Physics2D.Raycast(ramPosition, playerPosition - ramPosition, Vector2.Distance(ramPosition, playerPosition), layerMask);
        if(hit.collider != null && hit.collider.gameObject == targetPlayer.gameObject)
        {
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
            return true; // Обнаружен игрок
        }
        else
        {
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
            return false; // Игрок не обнаружен
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