using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Animator playerAnimator;
    private Quaternion _targetRotation;
    public Rigidbody2D rigidbody2d;


    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

    }

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
        _targetRotation = Quaternion.AngleAxis(angle, Vector3.back);
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, rotationSpeed * Time.deltaTime);
        rigidbody2d.velocity = transform.right * vector.magnitude * speed * Time.deltaTime;
    }
}
