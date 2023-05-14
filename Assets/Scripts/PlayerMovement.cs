using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Animator playerAnimator;
    public Rigidbody2D rigidbody2d;

    public void Update()
    {
        var vector = InputController.Instance.GetMoveVector();
        if (vector == Vector2.zero)
        {
            playerAnimator.SetBool("isIdle", true);
            return;
        }
        if(vector == Vector2.zero) return;
        playerAnimator.SetBool("isIdle", false);
        float angle = Mathf.Atan2(-vector.y, vector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        rigidbody2d.velocity = transform.right * vector.magnitude * speed * Time.deltaTime;
    }
}