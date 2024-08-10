using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class CustomPlayerInput : MonoBehaviour
{
    [Header("Character Input Values")] public Vector2 move;
    public Vector2 look;
    public bool attack;
    public bool jump;
    public bool walk;
    public bool cameraTurn;

    [Header("Movement Settings")] public bool analogMovement;

    [Header("Mouse Cursor Settings")] public bool cursorInputForLook;

    public void OnCameraTurn(InputValue value)
    {
        CameraTurnInput(value.isPressed);
        SetCursorState(value.isPressed);
    }

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }

    public void OnAttack(InputValue value)
    {
        AttackInput(value.isPressed);
    }

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void OnWalk(InputValue value)
    {
        WalkInput(value.isPressed);
    }

    private void CameraTurnInput(bool value)
    {
        cameraTurn = !cameraTurn;
    }

    private void SetCursorState(bool newState)
    {
        cursorInputForLook = !cursorInputForLook;
        Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void AttackInput(bool newAttackState)
    {
        attack = newAttackState;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void WalkInput(bool newValue)
    {
        walk = newValue;
    }
}