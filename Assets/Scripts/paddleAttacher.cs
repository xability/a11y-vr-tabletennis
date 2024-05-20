
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PaddleFlyToHand : MonoBehaviour
{
    public XRController controller; // XR Controller component attached to the right hand
    public GameObject paddle; // Reference to the paddle object

    void Start()
    {
        // Try to automatically find and assign the controller
        AssignControllerAutomatically();
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (controller == null)
        {
            Debug.LogError("Controller not assigned!");
            return;
        }

        // Assuming that 'A' button is the primary button
        if (controller.inputDevice.IsPressed(InputHelpers.Button.PrimaryButton, out bool isPressed) && isPressed)
        {
            // Paddle flies to hand
            paddle.transform.position = controller.transform.position;
        }
    }

    // This method attempts to find and assign the controller automatically
    void AssignControllerAutomatically()
    {
        var foundControllers = FindObjectsOfType<XRController>();
        foreach (var foundController in foundControllers)
        {
            // Check if this is the right-hand controller, you might want to check tags or names
            if (foundController.GetComponent<XRController>().controllerNode == XRNode.RightHand)
            {
                controller = foundController;
                break;
            }
            {
                controller = foundController;
                break;
            }
        }

        if (controller == null)
        {
            Debug.LogError("Right-hand controller could not be found. Please ensure it's in the scene and properly tagged or named.");
        }
    }
}
// working script ends here


/*
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PaddleGrabber : MonoBehaviour
{
    public InputHelpers.Button grabButton = InputHelpers.Button.PrimaryButton; // Default to A button
    public XRController controller; // XR Controller component attached to the right hand
    public float grabDistance = 0.1f; // Distance for the sphere cast
    public LayerMask interactableLayer; // Ensure this is set to the layer your paddle is on

    private XRBaseInteractable grabbedInteractable; // Reference to the grabbed interactable (paddle)

    void Start()
    {
        // Attempt to assign the controller at startup
        AssignController();
    }

    void Update()
    {
        if (controller == null)
        {
            Debug.LogWarning("Controller not assigned!");
            return;
        }

        if (!controller.inputDevice.isValid)
        {
            Debug.LogWarning("Input device not valid!");
            return;
        }

        if (controller.inputDevice.IsPressed(grabButton, out bool isPressed) && isPressed)
        {
            TryGrabPaddle();
        }
    }

    private void TryGrabPaddle()
    {
        RaycastHit hit;
        if (Physics.SphereCast(controller.transform.position, grabDistance, controller.transform.forward, out hit, grabDistance, interactableLayer))
        {
            if (hit.collider.CompareTag("Paddle"))
            {
                grabbedInteractable = hit.collider.GetComponent<XRBaseInteractable>();
                if (grabbedInteractable != null)
                {
                    AttachPaddleToController();
                }
            }
        }
    }

    private void AttachPaddleToController()
    {
        grabbedInteractable.transform.SetParent(controller.transform);
        grabbedInteractable.transform.localPosition = Vector3.zero; // Position relative to the controller
        grabbedInteractable.transform.localRotation = Quaternion.identity; // Align rotation with the controller
    }

    private void AssignController()
    {
        // Try to find the right hand controller by tag or name
        var rightHandController = GameObject.FindWithTag("GameController"); // Ensure this tag is correct

        if (rightHandController != null)
        {
            controller = rightHandController.GetComponent<XRController>();
            if (controller == null)
            {
                Debug.LogError("XRController component not found on the Right Hand Controller.");
            }
        }
        else
        {
            Debug.LogError("Failed to find Right Hand Controller object with tag 'GameController'.");
        }
    }
}
*/


// using UnityEngine;
// using UnityEngine.XR;
// using UnityEngine.XR.Interaction.Toolkit;

// public class PaddleGrabber : MonoBehaviour
// {
//     public InputHelpers.Button grabButton = InputHelpers.Button.PrimaryButton; // Default to A button
//     public XRController controller; // XR Controller component attached to the right hand
//     public float grabDistance = 0.1f; // Distance for the sphere cast
//     public LayerMask interactableLayer; // Ensure this is set to the layer your paddle is on

//     private XRBaseInteractable grabbedInteractable; // Reference to the grabbed interactable (paddle)

//     void Start()
//     {
//         // Assign the controller at startup
//         AssignController();
//     }

//     private void Update()
//     {
//         if (controller == null)
//         {
//             Debug.LogWarning("Controller not assigned!");
//             return;
//         }

//         if (!controller.inputDevice.isValid)
//         {
//             Debug.LogWarning("Input device not valid!");
//             return;
//         }

//         if (controller.inputDevice.IsPressed(grabButton, out bool isPressed) && isPressed)
//         {
//             TryGrabPaddle();
//         }
//     }

//     private void TryGrabPaddle()
//     {
//         // Perform a spherecast from the controller's position and forward direction
//         RaycastHit hit;
//         if (Physics.SphereCast(controller.transform.position, grabDistance, controller.transform.forward, out hit, grabDistance, interactableLayer))
//         {
//             // Check if the hit object has the "Paddle" tag
//             if (hit.collider.CompareTag("Paddle"))
//             {
//                 // Attempt to grab the paddle
//                 grabbedInteractable = hit.collider.GetComponent<XRBaseInteractable>();
//                 if (grabbedInteractable != null)
//                 {
//                     AttachPaddleToController();
//                 }
//             }
//         }
//     }

//     private void AttachPaddleToController()
//     {
//         // Attach paddle to the controller
//         grabbedInteractable.transform.SetParent(controller.transform);
//         grabbedInteractable.transform.localPosition = Vector3.zero; // Position relative to the controller
//         grabbedInteractable.transform.localRotation = Quaternion.identity; // Align rotation with the controller
//     }

//     private void AssignController()
//     {
//         // Automatically find the right hand controller by tag or name
//         var rightHandController = GameObject.FindWithTag("GameController");

//         if (controller == null)
//         {
//             Debug.LogError("Failed to find Right Hand Controller.");
//         }
//         controller = rightHandController.GetComponent<XRController>();
//     }
// }


/*
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PaddleGrabber : MonoBehaviour
{
    public InputHelpers.Button triggerButton; // Button to grab the paddle
    public XRController controller; // XR Controller component attached to the right hand

    private XRGrabInteractable grabbedInteractable; // Reference to the grabbed interactable (paddle)

    private void Update()
    {
        if (controller.inputDevice.IsPressed(triggerButton, out _) && grabbedInteractable == null)
        {
            GrabPaddle();
        }
    }

    private void GrabPaddle()
    {
        // Perform a spherecast from the controller's position and forward direction
        RaycastHit hit;
        if (Physics.SphereCast(controller.transform.position, 0.1f, controller.transform.forward, out hit))
        {
            // Check if the hit object has the "Paddle" tag
            if (hit.collider.CompareTag("Paddle"))
            {
                // Attempt to grab the paddle
                XRGrabInteractable interactable = hit.collider.GetComponent<XRGrabInteractable>();
                if (interactable != null)
                {
                    controller.GetComponent<XRController>().
                        .SelectInteractable(interactable);
                    grabbedInteractable = interactable;
                }
            }
        }
    }
}



/*
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PaddleAttacher : MonoBehaviour
{
    public XRBaseInteractor interactor; // The interaction object (e.g., XRDirectInteractor, XRSocketInteractor, etc.)
    public XRGrabInteractable paddle; // The paddle to attach

    private bool isAttached = false;

    private void Start()
    {
        interactor.selectEntered.AddListener(AttachPaddle);
        interactor.selectExited.AddListener(DetachPaddle);
    }

    private void AttachPaddle(SelectEnterEventArgs args)
    {
        if (!isAttached && args.interactable == paddle)
        {
            isAttached = true;
            paddle.transform.SetParent(interactor.transform);
            paddle.transform.localPosition = Vector3.zero;
            paddle.transform.localRotation = Quaternion.identity;
        }
    }

    private void DetachPaddle(SelectExitEventArgs args)
    {
        if (isAttached && args.interactable == paddle)
        {
            isAttached = false;
            paddle.transform.SetParent(null);
        }
    }
}
*/

/*
using UnityEngine; 
using UnityEngine.XR.Interaction.Toolkit;

public class PaddleAttacher : MonoBehaviour { 
    public XRDirectInteractor directInteractor; 
    [SerializeField] public XRGrabInteractable paddle;

    private bool isAttached = false;

    private void Start() { 
        directInteractor.onSelectEntered.AddListener(AttachPaddle); 
        directInteractor.onSelectExited.AddListener(DetachPaddle); 
    }

    private void AttachPaddle(XRBaseInteractable interactable) { 
        if (!isAttached && interactable == paddle) {
            isAttached = true; 
            paddle.transform.SetParent(directInteractor.transform); 
            paddle.transform.localPosition = Vector3.zero; 
            paddle.transform.localRotation = Quaternion.identity; 
        } 
    }

    private void DetachPaddle(XRBaseInteractable interactable) { 
        if (isAttached && interactable == paddle) { 
            isAttached = false; paddle.transform.SetParent(null); 
        } 
    } 
}


/*
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PaddleAttacher : MonoBehaviour
{
    public XRRayInteractor rayInteractor;
    public XRGrabInteractable paddle;

    private bool isAttached = false;

    private void Start()
    {
        rayInteractor.onSelectEntered.AddListener(AttachPaddle);
        rayInteractor.onSelectExited.AddListener(DetachPaddle);
    }

    private void AttachPaddle(XRBaseInteractable interactable)
    {
        if (!isAttached && interactable == paddle)
        {
            isAttached = true;
            paddle.transform.SetParent(rayInteractor.transform);
            paddle.transform.localPosition = Vector3.zero;
            paddle.transform.localRotation = Quaternion.identity;
        }
    }

    private void DetachPaddle(XRBaseInteractable interactable)
    {
        if (isAttached && interactable == paddle)
        {
            isAttached = false;
            paddle.transform.SetParent(null);
        }
    }
}
*/

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PaddleAttacher : MonoBehaviour
{
    public XRRayInteractor rayInteractor;
    public XRBaseInteractable paddle;

    private bool isAttached;

    private void Start()
    {
        rayInteractor.selectEntered.AddListener(OnSelectEntered);
        rayInteractor.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (!isAttached)
        {
            isAttached = true;
            paddle.transform.SetParent(rayInteractor.transform);
            paddle.transform.localPosition = Vector3.zero;
            paddle.transform.localRotation = Quaternion.identity;
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (isAttached)
        {
            isAttached = false;
            paddle.transform.SetParent(null);
        }
    }
}
*/