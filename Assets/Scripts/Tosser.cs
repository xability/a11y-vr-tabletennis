using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TosserXR : MonoBehaviour
{
    [SerializeField] private GameObject ball; // Reference to the ball prefab
    [SerializeField] private int reload = 4; // Length of time in between each toss, set a default value
    public float throwStrength = 10; // Strength of throws, set a default value
    private float time;
    private float autoStartTime = 10f; // Time in seconds before automatic start
    private bool autoStartTriggered = false; // Flag to track if automatic start has been triggered

    private List<Vector3> shotspos = new List<Vector3>(); // List of positions for the tosser to be at
    private List<Vector3> shotsrot = new List<Vector3>(); // List of rotations for the tosser to have

    private GameObject lastInstantiatedBall = null; // Reference to the last instantiated ball
    private bool canLaunch = false; // Flag to determine if the tosser can launch balls

    private GameObject rightController; // Reference to the right XR controller

    void Awake()
    {
        shotspos.Add(new Vector3(0.004889412f, 0.74f, 1.95f));
        shotsrot.Add(new Vector3(0f, 180f, 0f));
        // Add more shot positions and rotations if needed

        // Find the right XR controller GameObject
        rightController = GameObject.FindGameObjectWithTag("RightController");
        if (rightController == null)
        {
            Debug.LogError("RightController not found. Make sure you have tagged your right hand controller appropriately.");
        }
    }

    void Start()
    {
        // Initialize the timer
        time = reload;
    }

    void Update()
    {
        // Check if the paddle is being held by the right controller before allowing launches
        canLaunch = IsPaddleInteractedWith();

        // Decrement the timer only if the paddle is interacted with or if auto start has been triggered
        if (canLaunch || autoStartTriggered)
        {
            time -= Time.deltaTime;
        }
        else
        {
            // Countdown for automatic start if no interaction is detected
            autoStartTime -= Time.deltaTime;
            if (autoStartTime <= 0)
            {
                autoStartTriggered = true;
            }
        }

        // Check if it's time to toss the ball and if the paddle is interacted with
        if (time <= 0 && (canLaunch || autoStartTriggered))
        {
            // Delete the last instantiated ball before creating a new one
            if (lastInstantiatedBall != null)
            {
                Destroy(lastInstantiatedBall);
            }
            
            TossObject();
            // Reset the timer
            time = reload;
        }
    }

    private void TossObject()
    {
        int rand = Random.Range(0, shotspos.Count);
        transform.position = shotspos[rand];
        transform.eulerAngles = shotsrot[rand];

        // Instantiate a new ball and keep a reference to it
        lastInstantiatedBall = Instantiate(ball, transform.position, transform.rotation);
        lastInstantiatedBall.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * throwStrength);
    }

    private bool IsPaddleInteractedWith()
    {
        // Check if the paddle is interacted with by the right controller
        return (rightController != null && rightController.transform.childCount > 0); // Assuming the paddle is interacted with by the controller when held
    }
}



/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TosserXR : MonoBehaviour
{
    [SerializeField] private GameObject ball; // Reference to the ball prefab
    [SerializeField] private int reload = 4; // Length of time in between each toss, set a default value
    public float throwStrength = 10; // Strength of throws, set a default value
    private float time;
    public AudioSource launchSound; // Sound that plays every time a ball is tossed

    private List<Vector3> shotspos = new List<Vector3>(); // List of positions for the tosser to be at
    private List<Vector3> shotsrot = new List<Vector3>(); // List of rotations for the tosser to have

    private GameObject lastInstantiatedBall = null; // Reference to the last instantiated ball

    void Awake()
    {
        shotspos.Add(new Vector3(0.004889412f, 0.74f, 1.95f));
        shotsrot.Add(new Vector3(0f, 180f, 0f));
        /*
        // Initialize positions and rotations

        shotspos.Add(new Vector3(2f, 1.1f, 0.5f));
        shotsrot.Add(new Vector3(0f, 0f, 0f));

        shotspos.Add(new Vector3(2f, 1.1f, -0.5f));
        shotsrot.Add(new Vector3(0f, 0f, 0f));
        /
    }

    void Start()
    {
        // Initialize the timer
        time = reload;
    }

    void Update()
    {
        // Decrement the timer
        time -= Time.deltaTime;

        // Check if it's time to toss the ball
        if (time <= 0)
        {
            // Delete the last instantiated ball before creating a new one
            if (lastInstantiatedBall != null)
            {
                Destroy(lastInstantiatedBall);
            }
            
            TossObject();
            // Reset the timer
            time = reload;
        }
    }

    private void TossObject()
    {
        int rand = Random.Range(0, shotspos.Count);
        transform.position = shotspos[rand];
        transform.eulerAngles = shotsrot[rand];

        // Instantiate a new ball and keep a reference to it
        lastInstantiatedBall = Instantiate(ball, transform.position, transform.rotation);
        lastInstantiatedBall.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * throwStrength);

        if (launchSound != null)
        {
            launchSound.Play();
        }
    }
}
*/