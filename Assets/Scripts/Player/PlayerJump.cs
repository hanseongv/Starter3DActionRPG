using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerGroundedCheck))]
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] internal Vector3 JumpMotion;
        [Header("Jump")] public float jumpHeight = 1.2f;
        public float jumpTimeout = 0.40f;
        [Space(5)] [Header("Fall")] public float gravity = -15.0f;
        public float fallTimeout = 0.15f;

        #region Variables

        [SerializeField] internal float _verticalVelocity;
        private float _terminalVelocity;
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;
        private int _animIDJump;
        private int _animIDFreeFall;

        #endregion

        #region Components

        internal Animator animator;

        private CustomPlayerInput _input;
        private PlayerGroundedCheck _groundedCheck;

        #endregion

        internal void Init()
        {
            _terminalVelocity = 53.0f;
            _input = GetComponent<CustomPlayerInput>();
            _groundedCheck = GetComponent<PlayerGroundedCheck>();
            _groundedCheck.animator = animator;
            jumpTimeout = Mathf.Clamp(jumpTimeout, 0.2f, jumpTimeout);
            _jumpTimeoutDelta = jumpTimeout;
            _fallTimeoutDelta = fallTimeout;
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
        }

        internal void JumpAndFall()
        {
            if (_groundedCheck.grounded)
                Jump();
            else
                Fall();

            Gravity();

            JumpMotion = new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime;
        }

        private void Jump()
        {
            _fallTimeoutDelta = fallTimeout;

            animator.SetBool(_animIDFreeFall, false);
            if (_verticalVelocity < 0.0f)
                _verticalVelocity = -2f;

            UpdateJumpTimeout();
            if (_input.jump == false || 0.0f < _jumpTimeoutDelta) return;
            animator.ResetTrigger("doAttack");
            _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("doJump");
            _input.jump = false;
        }


        private void UpdateJumpTimeout()
        {
            if (0.0f <= _jumpTimeoutDelta) _jumpTimeoutDelta -= Time.deltaTime;
        }


        private void Fall()
        {
            _jumpTimeoutDelta = jumpTimeout;

            if (_fallTimeoutDelta >= 0.0f)
                _fallTimeoutDelta -= Time.deltaTime;
            else
                animator.SetBool(_animIDFreeFall, true);

            _input.jump = false;
        }

        private void Gravity()
        {
            if (_verticalVelocity < _terminalVelocity)
                _verticalVelocity += gravity * Time.deltaTime;
        }
    }
}