using UnityEngine;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [Header("Camera")] public GameObject cinemachineCameraTarget;
        public float topClamp = 70.0f;
        public float bottomClamp = -30.0f;
        public float cameraAngleOverride;
        // public bool lockCameraPosition;

        #region Variables

        private const float Threshold = 0.01f;
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        #endregion

        #region Components

        private StarterAssetsInputs _input;

        #endregion

        internal void Init()
        {
            _cinemachineTargetYaw = cinemachineCameraTarget.transform.rotation.eulerAngles.y;
            _input = GetComponent<StarterAssetsInputs>();
        }

        internal void CameraRotation()
        {
            // lockCameraPosition = !_input.cameraTurn;
            // if (_input.cameraTurn == false) return;
            if (_input.look.sqrMagnitude >= Threshold && _input.cameraTurn)
            {
                var deltaTimeMultiplier = 10.0f * Time.deltaTime;
                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.look.y * -deltaTimeMultiplier;
            }

            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, bottomClamp, topClamp);
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);

            cinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + cameraAngleOverride, _cinemachineTargetYaw, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}