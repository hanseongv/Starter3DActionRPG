using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerJump))]
    [RequireComponent(typeof(PlayerCamera))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Components")] public Animator animator;

        #region Components

        private PlayerMovement _playerMovement;
        private PlayerJump _playerJump;
        private PlayerCamera _playerCamera;
        private CharacterController _controller;

        #endregion

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerJump = GetComponent<PlayerJump>();
            _playerCamera = GetComponent<PlayerCamera>();
            _playerMovement.animator = animator;
            _playerJump.animator = animator;
            _playerMovement.Init();
            _playerJump.Init();
            _playerCamera.Init();
        }

        private void Update()
        {
            _playerJump.JumpAndGravity();
            _playerMovement.Move();
            _controller.Move(_playerMovement.MoveMotion + _playerJump.JumpMotion);
        }

        private void LateUpdate()
        {
            _playerCamera.CameraRotation();
        }
    }
}