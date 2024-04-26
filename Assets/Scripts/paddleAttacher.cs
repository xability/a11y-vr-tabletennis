using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public enum State { InHand , Idle , grab }

public class paddleAttacher : MonoBehaviour {
    State state;
    //rb= GetComponent<Rigidbody>();

    //public float padSpeed = 1.0f;
    //public XRGrabInteractable xrGrabInteractable; // Reference to XR Grab Interactable component
    [SerializeField] private Transform playerHand;
    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        switch(state) {
            case State.InHand:
                {
                    Debug.Log("you are already holding the paddle");
                }
                break;
            case State.grab: 
                {
                    if (Vector3.SqrMagnitude(playerHand.position - transform.position) > 16)
                    {
                        Vector3 dir = (playerHand.position - transform.position).normalized;
                        //GrabPaddle();
                    }
                    else { 
                        state = State.Idle;
                    }
                }
                break;
            case State.Idle:
                {
                    Debug.Log("you are not holding the paddle");
                }
                break;
            default:
                break;
        }
    }

    public void HoldPaddle() {
        state = State.InHand;    
    }
    public void IdelPosition() { 
        state = State.Idle;
    }
    public void grab() { 
        state = State.grab;
    }

    /*
    [System.Obsolete]
    public void HoldGrab()
    {
        xrGrabInteractable.selectingInteractor = null; // Release the paddle from any previous interactions
        xrGrabInteractable.AttachToHand(playerHand.gameObject); // Attach the paddle to the hand
        state = State.InHand; // Update state to indicate the paddle is grabbed
    }
    */
}

/*
 * doesnt work either ;(
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
*/


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