using UnityEngine;
using Bhaptics.SDK2;
using VRTableTennis.Core;

namespace VRTableTennis.Accessibility
{
    /// <summary>
    /// Provides haptic feedback for BLV users based on ball position and distance
    /// </summary>
    public class HapticsController : MonoBehaviour
    {
        [Header("Game Objects")]
        [SerializeField] private GameObject ball;
        public GameObject player;
        
        [Header("Table Settings")]
        public float tableHeight = 0.232f;
        
        [Header("Distance Settings")]
        public float minDistance = 0.5f;
        public float maxDistance = 1.7f;
        
        [Header("Haptic Intensity")]
        public int minIntensity = 1;
        public int maxIntensity = 100;
        public int durationMillis = 250;

        void Awake()
        {
            InitializeHaptics();
            ValidateReferences();
        }

        void Update()
        {
            if (IsValidSetup())
            {
                UpdateHapticFeedback();
            }
        }

        private void InitializeHaptics()
        {
            BhapticsLibrary.Play(BhapticsEvent.GLOVE_CHECK);
            Debug.Log("Glove check played");
        }

        private void ValidateReferences()
        {
            if (player == null || ball == null)
            {
                Debug.LogError("Necessary game objects not found. Please ensure your objects are correctly tagged.");
                this.enabled = false;
            }
        }

        private bool IsValidSetup()
        {
            return player != null && ball != null;
        }

        private void UpdateHapticFeedback()
        {
            float distance = Vector3.Distance(player.transform.position, ball.transform.position);
            int intensity = CalculateHapticIntensity(distance);
            int activeMotors = CalculateActiveMotors(ball.transform.position.y - tableHeight);
            PlayHapticFeedback(intensity, activeMotors);
        }

        private int CalculateHapticIntensity(float distance)
        {
            if (maxDistance <= minDistance)
            {
                Debug.LogError("maxDistance must be greater than minDistance!");
                return minIntensity;
            }

            if (distance <= maxDistance && distance >= minDistance)
            {
                float normalizedDistance = (distance - minDistance) / (maxDistance - minDistance);
                float intensity = Mathf.Lerp(maxIntensity, minIntensity, normalizedDistance);
                return Mathf.RoundToInt(intensity);
            }
            else if (distance < minDistance)
            {
                return maxIntensity;
            }
            else
            {
                return minIntensity;
            }
        }

        private int CalculateActiveMotors(float height)
        {
            float playerHeight = player.transform.position.y - tableHeight;
            if (height <= 0.2f * playerHeight) return 4; // Only little finger
            if (height <= 0.4f * playerHeight) return 3; // Little finger and ring finger
            if (height <= 0.6f * playerHeight) return 2; // Little, ring, and middle fingers
            if (height <= 0.8f * playerHeight) return 1; // Little, ring, middle, and index fingers
            return 5; // All fingers (excluding thumb and wrist)
        }

        private void PlayHapticFeedback(int intensity, int activeMotors)
        {
            int normalizedIntensity = Mathf.Clamp(intensity, minIntensity, maxIntensity);
            int[] motorValues = new int[6];

            for (int i = 0; i < motorValues.Length; i++)
            {
                if (i < activeMotors || i == 4) // include the wrist motor in the intensity calculation
                {
                    motorValues[i] = normalizedIntensity;
                }
                else
                {
                    motorValues[i] = 0;
                }
            }

            BhapticsLibrary.PlayMotors((int)PositionType.GloveR, motorValues, durationMillis);
        }
    }
} 