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
                    controller.GetComponent<XRController>().SelectInteractable(interactable);
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