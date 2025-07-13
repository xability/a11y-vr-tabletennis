using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using VRTableTennis.Core;

namespace VRTableTennis.Gameplay
{
    /// <summary>
    /// Handles ball physics, collision detection, and audio feedback for the table tennis ball
    /// </summary>
    public class Ball : MonoBehaviour
    {
        [Header("Audio Sources")]
        public AudioSource tableBounce;
        public AudioSource paddleBounce;
        public AudioSource airSound;
        public AudioSource bodyHit;

        [Header("References")]
        public GameObject player;

        [Header("Haptic Settings")]
        private XRController rightController;
        public float hapticIntensity = 0.7f;

        void Awake()
        {
            InitializeAudioSources();
            InitializeController();
        }

        private void InitializeAudioSources()
        {
            AudioSource[] audios = GetComponents<AudioSource>();

            tableBounce = audios.Length > 0 ? audios[0] : throw new System.IndexOutOfRangeException("TableBounce AudioSource not found.");
            paddleBounce = audios.Length > 1 ? audios[1] : throw new System.IndexOutOfRangeException("PaddleBounce AudioSource not found.");
            airSound = audios.Length > 2 ? audios[2] : throw new System.IndexOutOfRangeException("AirSound AudioSource not found.");
            bodyHit = audios.Length > 3 ? audios[3] : throw new System.IndexOutOfRangeException("BodyHit AudioSource not found.");
        }

        private void InitializeController()
        {
            rightController = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<XRController>();
            if (rightController == null)
            {
                Debug.LogError("RightController not found. Make sure you have tagged your right hand controller appropriately.");
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            HandleCollision(collision);
        }

        private void HandleCollision(Collision collision)
        {
            if (collision.gameObject.CompareTag("Table"))
            {
                PlayTableBounce();
            }
            else if (collision.gameObject.CompareTag("Paddle"))
            {
                PlayPaddleBounce();
                TriggerHapticFeedback();
            }
            else if (collision.gameObject.CompareTag("Player"))
            {
                PlayBodyHit();
            }
        }

        private void PlayTableBounce()
        {
            if (tableBounce != null)
            {
                tableBounce.Play();
            }
        }

        private void PlayPaddleBounce()
        {
            if (paddleBounce != null)
            {
                paddleBounce.Play();
            }
        }

        private void PlayBodyHit()
        {
            if (bodyHit != null)
            {
                bodyHit.Play();
            }
            else
            {
                Debug.LogWarning("BodyHit AudioSource is null. Cannot play audio.");
            }
        }

        private void TriggerHapticFeedback()
        {
            if (rightController != null)
            {
                rightController.SendHapticImpulse(0, hapticIntensity);
            }
            else
            {
                Debug.LogWarning("RightController is null. Haptic feedback cannot be triggered.");
            }
        }
    }
} 