using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRam_Move : MonoBehaviour
{
    public Transform targetPlayer;
    public LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        CheackRenderLine();
        Debug.DrawLine(transform.position, targetPlayer.position); // проводит линию между Ram и игроком
    }


    public void CheackRenderLine()//проверка видет ли противник , игрока , проверка производитья через  Line render 
    {
        Vector2 ramPosition = transform.position;
        Vector2 playerPosition = targetPlayer.position;

        RaycastHit2D hit = Physics2D.Raycast(ramPosition, playerPosition - ramPosition, Vector2.Distance(ramPosition, playerPosition));

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
}