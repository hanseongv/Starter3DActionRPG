using System;
using Define;
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

        #endregion

        #region Components

        private Animator _animator;
        private PlayerAudio _playerAudio;
        internal PlayerMovement _playerMovement;
        private PlayerJump _playerJump;
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
            if (isAttack) return;

            _playerJump.JumpAndFall();
            _playerMovement.Move();
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
    }
}