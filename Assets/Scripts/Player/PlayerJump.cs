using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerGroundedCheck))]
    public class PlayerJump : MonoBehaviour
    {
        internal Vector3 JumpMotion;
        [Header("Jump")] public float jumpHeight = 1.2f;
        public float jumpTimeout = 0.50f;
        [Space(5)] [Header("Fall")] public float gravity = -15.0f;
        public float fallTimeout = 0.15f;

        #region Variables

        private float _verticalVelocity;
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
            _jumpTimeoutDelta = jumpTimeout;
            _fallTimeoutDelta = fallTimeout;
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
        }

        internal void JumpAndGravity()
        {
            if (_groundedCheck.grounded)
            {
                _fallTimeoutDelta = fallTimeout;

                if (animator)
                {
                    animator.SetBool(_animIDJump, false);
                    animator.SetBool(_animIDFreeFall, false);
                }

                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

                    if (animator)
                    {
                        animator.SetBool(_animIDJump, true);
                    }
                }

                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                _jumpTimeoutDelta = jumpTimeout;

                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    animator.SetBool(_animIDFreeFall, true);
                }

                _input.jump = false;
            }

            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += gravity * Time.deltaTime;
            }

            JumpMotion = new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime;
        }
    }
}