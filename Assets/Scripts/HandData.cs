using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class HandData : MonoBehaviour
{
    public enum HandModelType { Left, Right }

    public HandModelType handType;
    public Transform root;
    public Animator animator;
    public Transform[] fingerBones;

    public XRController controller; // Reference to the XRController
    public UnityEvent onPressAButton; // Unity event to trigger when A button is pressed

    void Update()
    {
        // Check for button press on the specified XRController
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
        {
            // Invoke the Unity event when A button is pressed
            onPressAButton.Invoke();
        }
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandData : MonoBehaviour
{
    public enum HandModelType{Left , Right}

    public HandModelType handType;
    public Transform root;
    public Animator animator;
    public Transform[] fingerBones;
}
*/