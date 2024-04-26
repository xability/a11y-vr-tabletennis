
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator;

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}




/*

using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionReference pickupAction;
    public InputActionReference pinchAnimationAction;
    public InputActionReference gripAnimationAction;
    public Animator handAnimator;
    public float pickupDistance = 0.1f; // Distance threshold for picking up the paddle
    public LayerMask paddleLayerMask; // Layer mask for the paddle GameObjects

    private bool isPaddlePickedUp = false;
    private GameObject pickedUpPaddle;
    private bool isButtonPressed = false;
    private float buttonPressTime = 0f;

    private void OnEnable()
    {
        pickupAction.action.performed += OnPickupAction;
    }

    private void OnDisable()
    {
        pickupAction.action.performed -= OnPickupAction;
    }

    private void OnPickupAction(InputAction.CallbackContext context)
    {
        isButtonPressed = context.ReadValueAsButton();
        buttonPressTime = Time.time;

        if (isButtonPressed && Time.time - buttonPressTime < 1f)
        {
            if (!isPaddlePickedUp)
            {
                // Check for nearby paddle to pick up
                Collider[] colliders = Physics.OverlapSphere(transform.position, pickupDistance, paddleLayerMask);
                if (colliders.Length > 0)
                {
                    // Pick up the first found paddle
                    pickedUpPaddle = colliders[0].gameObject;
                    PickupPaddle(pickedUpPaddle);
                }
            }
            else
            {
                // Drop the picked up paddle
                DropPaddle();
            }
        }
    }

    private void Update()
    {
        // Update hand animation parameters
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }

    private void PickupPaddle(GameObject paddle)
    {
        // Move the paddle to the hand position
        paddle.transform.position = transform.position;
        paddle.transform.rotation = transform.rotation;
        isPaddlePickedUp = true;
    }

    private void DropPaddle()
    {
        if (pickedUpPaddle != null)
        {
            // Reset paddle to its original position and rotation
            pickedUpPaddle.transform.position = pickedUpPaddle.GetComponent<Transform>().position;
            pickedUpPaddle.transform.rotation = pickedUpPaddle.GetComponent<Transform>().rotation;
            isPaddlePickedUp = false;
        }
    }
}
*/
