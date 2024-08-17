using System.Collections;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerAttack : MonoBehaviour
    {
        #region Components

        internal PlayerController controller;
        internal Animator animator;
        private CustomPlayerInput _input;
        private PlayerGroundedCheck _groundedCheck;

        #endregion

        private float attackCoolTime = 0.0f;
        private float attackCoolTimeMax = 2.0f;
        private IEnumerator _attackCoolTimeRoutine;
        private IEnumerator _battleRoutine;

        internal void Init()
        {
            _input = GetComponent<CustomPlayerInput>();
            _groundedCheck = GetComponent<PlayerGroundedCheck>();
            _groundedCheck.animator = animator;
        }

        internal void Attack()
        {
            // animator.SetBool("isAttack", false);

            if (CanAttack() == false) return;
            // animator.SetBool("Jump", false);
            // animator.SetBool("FreeFall", false);
            // _input.jump = false;
            // controller._playerJump.StopJump();
            _input.attack = false;
            // controller.isAttack = true;
            // OnAttackCoolTime();
            OnStateBattle();
            // animator.ResetTrigger("doJump");

            animator.SetTrigger("doAttack");
            // animator.SetBool("isAttack", true);
        }

        private bool CanAttack()
        {
            return _input.attack && _groundedCheck.grounded && attackCoolTime == 0 && !_input.jump;
            // return _input.attack && _groundedCheck.grounded && attackCoolTime == 0 && !controller.isJump;
            
            // && controller._playerJump.JumpMotion.y == 0.0f;
        }

        private void OnAttackCoolTime()
        {
            if (_attackCoolTimeRoutine != null) StopCoroutine(_attackCoolTimeRoutine);
            _attackCoolTimeRoutine = AttackCoolTimeRoutine();
            StartCoroutine(_attackCoolTimeRoutine);
        }


        private IEnumerator AttackCoolTimeRoutine()
        {
            attackCoolTime = attackCoolTimeMax;
            while (0 < attackCoolTime)
            {
                attackCoolTime -= Time.deltaTime;
                yield return null;
            }

            attackCoolTime = 0;
        }

        private void OnStateBattle()
        {
            if (_battleRoutine != null) StopCoroutine(_battleRoutine);
            _battleRoutine = BattleRoutine();
            StartCoroutine(_battleRoutine);
        }


        private IEnumerator BattleRoutine()
        {
            animator.SetBool("isBattle", true);
            yield return new WaitForSeconds(8.0f);
            animator.SetBool("isBattle", false);
        }
    }
}