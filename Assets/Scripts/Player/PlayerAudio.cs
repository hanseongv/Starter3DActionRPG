using UnityEngine;

namespace Player
{
    public class PlayerAudio : MonoBehaviour
    {
        public AudioClip landingAudioClip;
        public AudioClip[] footstepAudioClips;
        [Range(0, 1)] public float footstepAudioVolume = 0.5f;

        private CharacterController _controller;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        public void HandleFootsteps()
        {
        }

        public void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (footstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, footstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(footstepAudioClips[index], transform.TransformPoint(_controller.center), footstepAudioVolume);
                }
            }
        }

        public void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(landingAudioClip, transform.TransformPoint(_controller.center), footstepAudioVolume);
            }
        }
    }
}