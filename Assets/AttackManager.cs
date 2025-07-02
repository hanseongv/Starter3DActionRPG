using Player;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private PlayerController _controller;

    private void Start()
    {
        _controller = GetComponentInParent<PlayerController>();
    }

    public float moveDistance = 1f; // 이동할 거리
    public float moveDuration = 0.05f;

    private void StartAttack()
    {
        _controller.Dash(moveDistance, moveDuration);
    }

    private void HitPoint()
    {
    }

    private void EndAttack()
    {
        _controller._animator.ResetTrigger("doAttack");
    }
}