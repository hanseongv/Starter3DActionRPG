using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(StarterAssetsInputs))]
    [RequireComponent(typeof(PlayerAudio))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        internal Vector3 MoveMotion;

        [Header("Move")] public float moveSpeed = 2.0f;
        public float sprintSpeed = 5.335f;
        public float rotationSmoothTime = 0.12f;
        public float speedChangeRate = 10.0f;

        #region Variables

        private float _animationBlend;
        private float _speed;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private int _animIDSpeed;
        private int _animIDMotionSpeed;

        #endregion

        #region Components

        private StarterAssetsInputs _input;
        private GameObject _mainCamera;
        private PlayerAudio _playerAudio;
        private Animator _animator;

        #endregion


        internal void Init()
        {
            _input = GetComponent<StarterAssetsInputs>();
            _playerAudio = GetComponent<PlayerAudio>();
            _animator = GetComponent<Animator>();

            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }


        internal void Move()
        {
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            float targetSpeed = _input.sprint ? sprintSpeed : moveSpeed;
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;
            var currentHorizontalSpeed = inputDirection.magnitude * targetSpeed;
            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            if (currentHorizontalSpeed < targetSpeed - speedOffset || targetSpeed + speedOffset < currentHorizontalSpeed)
            {
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * speedChangeRate);
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            if (_input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                var rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, rotationSmoothTime);
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            var targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            MoveMotion = targetDirection.normalized * (_speed * Time.deltaTime);
            _animator.SetFloat(_animIDSpeed, _animationBlend);
            _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            _playerAudio?.HandleFootsteps();
        }
    }
}