using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private float speed = 10;
    
    private void Start()
    {
        rigidbody2d.velocity = transform.right * speed;
    } 

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Player"){
            Destroy(gameObject);
        }
        var normal = col.contacts[0].normal; // Нормаль к поверхности стены
        var startSpeed = rigidbody2d.velocity.magnitude;

        // Вычисляем новое направление движения пули
        var reflect = Vector2.Reflect(rigidbody2d.velocity, normal);

        transform.right = reflect.normalized;

        rigidbody2d.velocity = transform.right * startSpeed;
    }
}