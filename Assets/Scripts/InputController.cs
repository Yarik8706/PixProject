using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Joystick moveJoystick;
    [SerializeField] private Joystick attackJoystick;

    public static InputController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Vector2 GetMoveVector()
    {
        return new Vector2(moveJoystick.Horizontal, moveJoystick.Vertical);
    }

    public Vector2 GetAttackVector()
    {
        return new Vector2(attackJoystick.Horizontal, attackJoystick.Vertical);
    }
}