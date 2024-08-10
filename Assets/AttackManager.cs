using System;
using Player;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private PlayerController _controller;

    private void Start()
    {
        _controller = GetComponentInParent<PlayerController>();
    }

    private void StartAttack()
    {
        _controller.isAttack = true;
    }

    private void HitPoint()
    {
    }

    private void EndAttack()
    {
        _controller.isAttack = false;
    }
}