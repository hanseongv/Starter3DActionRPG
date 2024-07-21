using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
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

        private Animator _animator;

        #endregion

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animIDGrounded = Animator.StringToHash("Grounded");
        }

        private void Update()
        {
            GroundedCheck();
        }

        private void GroundedCheck()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
            grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);

            _animator.SetBool(_animIDGrounded, grounded);
        }
    }
}