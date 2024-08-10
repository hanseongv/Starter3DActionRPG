using UnityEngine;

namespace Player
{
    public class PlayerGroundedCheck : MonoBehaviour
    {
        [Header("GroundedCheck")] [SerializeField]
        internal bool grounded = true;

        public float groundedOffset = -0.14f;
        public float groundedRadius = 0.28f;
        public LayerMask groundLayers;

        #region Variables

        private int _animIDGrounded;

        #endregion

        #region Components

        internal Animator animator;

        #endregion

        private void Start()
        {
            _animIDGrounded = Animator.StringToHash("Grounded");
        }

        private void Update()
        {
            GroundedCheck();
        }

        private void GroundedCheck()
        {
            var spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
            grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);

            animator.SetBool(_animIDGrounded, grounded);
        }
    }
}