using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class CustomPlayerInput : MonoBehaviour
{
    [Header("Character Input Values")] public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool walk;
    public bool cameraTurn;

    [Header("Movement Settings")] public bool analogMovement;

    [Header("Mouse Cursor Settings")] public bool cursorLocked;
    public bool cursorInputForLook;

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

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void OnWalk(InputValue value)
    {
        WalkInput(value.isPressed);
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void WalkInput(bool newValue)
    {
        walk = newValue;
    }

    private void CameraTurnInput(bool value)
    {
        cameraTurn = value;
    }

    private void SetCursorState(bool newState)
    {
        cursorInputForLook = newState;
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}