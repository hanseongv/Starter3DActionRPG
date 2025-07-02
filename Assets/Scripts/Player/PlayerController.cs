using System;
using Define;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerJump))]
    [RequireComponent(typeof(PlayerCamera))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Character")] [SerializeField] private GameObject character;

        #region Variables

        private bool _isLockMotion;
        [SerializeField] internal bool isAttack;
        [SerializeField] internal bool isJump;

        #endregion

        #region Components

        internal Animator _animator;
        private PlayerAudio _playerAudio;
        internal PlayerMovement _playerMovement;
        internal PlayerJump _playerJump;
        private PlayerCamera _playerCamera;
        internal CharacterController _controller;
        private PlayerAttack _attack;

        #endregion


        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerJump = GetComponent<PlayerJump>();
            _playerCamera = GetComponent<PlayerCamera>();
            _animator = character.GetComponent<Animator>();
            _playerAudio = character.GetComponent<PlayerAudio>();
            _attack = GetComponent<PlayerAttack>();
            _attack.animator = _animator;
            _attack.controller = this;
            _playerAudio.controller = _controller;
            _playerMovement.animator = _animator;
            _playerJump.animator = _animator;
            _attack.Init();
            _playerMovement.Init();
            _playerJump.Init();
            _playerCamera.Init();
        }

        private void Update()
        {
            if (_isLockMotion) return;
         
            _attack.Attack();
            
            if (_isDashing) return;

            _playerMovement.Move();

            _playerJump.JumpAndFall();

            if (isAttack)
            {
                _playerMovement.MoveMotion = Vector3.zero;
            }

            _controller.Move(_playerMovement.MoveMotion + _playerJump.JumpMotion);
        }


        private void LateUpdate()
        {
            _playerCamera.CameraRotation();
        }

        private void FixedUpdate()
        {
            _animator.SetFloat(AnimatorHashes.StateTime, Mathf.Repeat(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f));
        }


        private Vector3 _dashDirection;
        private float _dashTimeRemaining;
        private bool _isDashing;

        internal void Dash(float moveDistance, float moveDuration)
        {
            var targetPosition = _controller.transform.position + _controller.transform.forward * moveDistance;
            _dashDirection = (targetPosition - _controller.transform.position).normalized;

            DOTween.To(() => 0f, x => MoveCharacter(_dashDirection, x * moveDistance / moveDuration), moveDistance, moveDuration)
                .SetEase(Ease.OutExpo)
                .OnComplete(() => _isDashing = false);

            _isDashing = true;
            _dashTimeRemaining = moveDuration;
        }

        private void MoveCharacter(Vector3 direction, float distance)
        {
            if (_dashTimeRemaining > 0)
            {
                // 충돌 감지를 유지하며 이동
                _controller.Move(direction * distance * Time.deltaTime);
                _dashTimeRemaining -= Time.deltaTime;
            }
            else
            {
                _isDashing = false;
            }
        }
    }
}