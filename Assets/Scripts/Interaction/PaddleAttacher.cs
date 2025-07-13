using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using VRTableTennis.Core;

namespace VRTableTennis.Interaction
{
    /// <summary>
    /// Handles paddle interaction, pickup mechanics, and audio feedback for paddle location
    /// </summary>
    public class PaddleAttacher : MonoBehaviour
    {
        [Header("Controller Settings")]
        [SerializeField] private XRController rightHandController;
        
        [Header("Paddle Settings")]
        public GameObject paddle;
        public AudioSource audioSource;
        
        [Header("State")]
        private bool isPaddlePickedUp = false;

        void Update()
        {
            HandleInput();
        }

        void HandleInput()
        {
            if (rightHandController == null)
            {
                Debug.LogError("Controller not assigned!");
                return;
            }

            if (IsTriggerPressed() && !isPaddlePickedUp)
            {
                CheckForPaddlePickup();
            }
        }

        private bool IsTriggerPressed()
        {
            rightHandController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);
            return triggerPressed;
        }

        private void CheckForPaddlePickup()
        {
            Ray ray = new Ray(rightHandController.transform.position, rightHandController.transform.forward);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == paddle)
                {
                    PaddlePickedUp();
                }
            }
        }

        void PaddlePickedUp()
        {
            isPaddlePickedUp = true;
            StopPaddleAudio();
        }

        private void StopPaddleAudio()
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
                Debug.Log("Audio stopped as the paddle was picked up.");
            }
        }

        public void ResetPaddlePickup()
        {
            isPaddlePickedUp = false;
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
                Debug.Log("Audio should start again.");
            }
        }
    }
} 