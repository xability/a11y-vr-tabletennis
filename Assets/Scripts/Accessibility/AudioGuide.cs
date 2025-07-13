using UnityEngine;
using VRTableTennis.Core;

namespace VRTableTennis.Accessibility
{
    /// <summary>
    /// Provides audio guidance for BLV users to locate the paddle in the game environment
    /// </summary>
    public class AudioGuide : MonoBehaviour
    {
        [Header("Player Settings")]
        public Transform player;
        
        [Header("Audio Settings")]
        public AudioSource beepingSound;
        public float activationDistance = 3.0f;
        
        [Header("State")]
        private bool isHeld = false;

        void Update()
        {
            if (!isHeld)
            {
                UpdateAudioGuidance();
            }
        }

        private void UpdateAudioGuidance()
        {
            float distance = Vector3.Distance(transform.position, player.position);
            
            if (distance <= activationDistance)
            {
                PlayBeepingSound();
            }
            else
            {
                StopBeepingSound();
            }
        }

        private void PlayBeepingSound()
        {
            if (!beepingSound.isPlaying)
            {
                beepingSound.Play();
            }
        }

        private void StopBeepingSound()
        {
            if (beepingSound.isPlaying)
            {
                beepingSound.Stop();
            }
        }

        /// <summary>
        /// Called when the paddle is picked up to stop audio guidance
        /// </summary>
        public void OnPaddlePickedUp()
        {
            isHeld = true;
            StopBeepingSound();
        }
    }
} 