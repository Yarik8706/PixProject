using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamMovement : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask blockingLayer;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float attackSpeed = 10f;// more than moveSpeed :)))) this for me Kola
    [SerializeField] private float stopDistance = 1f;
    private Collider2D _boxCollider2D;
    private int curectPoint = 0;
    private bool isAttack;

    private void Start()
    {
        _boxCollider2D = GetComponent<Collider2D>();
    }


    private void Update()
    {
        CheckPlayerReach();
        MoveOnPoint();
        Debug.DrawLine(transform.position, playerTransform.position);
    }


    private bool CheckPlayerReach()
    {
        bool playerPeach = false;
        Vector3 playerDirection = playerTransform.position - transform.position;
        float angel = Vector3.Angle(transform.right, playerDirection);

        if (angel <= 90)
        {
            RaycastHit2D hit = Physics2D.Linecast(playerTransform.position, transform.position, blockingLayer);
            if (hit.collider == null)
            {
                return true;
            }
        }
        return playerPeach;
    }


    private void MoveOnPoint()
    {
        if (curectPoint < points.Length && !isAttack)
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
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            transform.position = transform.position + direction * moveSpeed * Time.deltaTime;

            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= stopDistance)
            {
                transform.position = playerTransform.position - direction * stopDistance;
                if (targetRotation == transform.rotation)
                {
                    StartCoroutine(AttackCoroutine(playerTransform.position + direction * 3));
                }
            }

            if (CheckPlayerReach())
            {
                float distanseToPoints = Vector3.Distance(transform.position, playerTransform.position);
                //if (distanseToPoints <= 0.1f)
                //{
                //тута будет анимЭйшен
                //}
            }
            else
            {
                if (Vector3.Distance(transform.position, curectOnePoint.position) < 0.1f)
                {
                    curectPoint = RandomPoints();
                    if (curectPoint >= points.Length)
                    {
                        curectPoint = 0;
                    }
                }
            }
        }
    }

    private IEnumerator AttackCoroutine(Vector3 newPosition)
    {
        _boxCollider2D.isTrigger = true;
        isAttack = true;
        yield return new WaitForSeconds(0.7f);

        float distanceToNewPosition = Vector3.Distance(transform.position, newPosition);
        Vector3 direction = (newPosition - transform.position).normalized;

        while(distanceToNewPosition > 0.1f && isAttack)
        {
            transform.position = transform.position + direction * attackSpeed* Time.deltaTime;
            distanceToNewPosition = Vector3.Distance(transform.position, newPosition);
            yield return null;
        }

        yield return new WaitForSeconds(5);
        isAttack = false;
        _boxCollider2D.isTrigger = false;
    }

    private int RandomPoints()
    {
        System.Random random = new System.Random();
        return random.Next(0, points.Length);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.attachedRigidbody.gameObject.CompareTag("Player"))
        {
            isAttack = false;
        }
    }
}

    