using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using VRTableTennis.Core;

namespace VRTableTennis.Interaction
{
    /// <summary>
    /// Manages hand tracking data and controller input for VR interactions
    /// </summary>
    public class HandData : MonoBehaviour
    {
        [Header("Hand Settings")]
        public enum HandModelType { Left, Right }
        public HandModelType handType;
        
        [Header("Hand Components")]
        public Transform root;
        public Animator animator;
        public Transform[] fingerBones;
        
        [Header("Controller")]
        public XRController controller;
        public UnityEvent onPressAButton;

        void Update()
        {
            CheckForButtonPress();
        }

        private void CheckForButtonPress()
        {
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            {
                onPressAButton.Invoke();
            }
        }
    }
} 