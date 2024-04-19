using UnityEngine; 
using UnityEngine.XR.Interaction.Toolkit;

public class PaddleAttacher : MonoBehaviour { 
    public XRDirectInteractor directInteractor; 
    public XRGrabInteractable paddle;

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