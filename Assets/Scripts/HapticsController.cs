using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bhaptics.SDK2;

public class HapticsController : MonoBehaviour
{
    [SerializeField] private GameObject ball; // Reference to the ball
    public GameObject player; // Reference to the player (OpenXR rig)
    public float tableHeight = 0.232f; // Height of the table surface

    // Distance range for haptic intensity
    public float minDistance = 0.5f;
    public float maxDistance = 1.7f;

    // Minimum and maximum haptic intensity
    public int minIntensity = 1;
    public int maxIntensity = 100;

    // Duration of the haptic feedback in milliseconds
    public int durationMillis = 250;

    void Awake()
    {
        BhapticsLibrary.Play(BhapticsEvent.GLOVE_CHECK);
        Debug.Log("Glove check played");

        //player = GameObject.FindGameObjectWithTag("Player");
        //ball = GameObject.FindGameObjectWithTag("sceneBall");  // Ensure you have a "Ball" tag assigned to your ball GameObject

        if (player == null || ball == null)
        {
            Debug.LogError("Necessary game objects not found. Please ensure your objects are correctly tagged.");
            this.enabled = false; // Disable the script to prevent further errors if objects are not found
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, ball.transform.position);
        int intensity = CalculateHapticIntensity(distance);
        int activeMotors = CalculateActiveMotors(ball.transform.position.y - tableHeight);
        PlayHapticFeedback(intensity, activeMotors);
    }

    int CalculateHapticIntensity(float distance)
    {
        Debug.Log($"Distance: {distance}, Min Distance: {minDistance}, Max Distance: {maxDistance}");
         
        if (maxDistance <= minDistance)
        {
            Debug.LogError("maxDistance must be greater than minDistance!");
            return minIntensity;
        }

        if (distance <= maxDistance || distance >= minDistance)
        {
            float normalizedDistance = (distance - minDistance) / (maxDistance - minDistance);
            float intensity = Mathf.Lerp(maxIntensity, minIntensity, normalizedDistance);

            Debug.Log($"Normalized Distance: {normalizedDistance}");
            Debug.Log($"Calculated Intensity: {intensity}");

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

    int CalculateActiveMotors(float height)
    {
        float playerHeight = player.transform.position.y - tableHeight;
        if (height <= 0.2f * playerHeight) return 4; // Only little finger
        if (height <= 0.4f * playerHeight) return 3; // Little finger and ring finger
        if (height <= 0.6f * playerHeight) return 2; // Little, ring, and middle fingers
        if (height <= 0.8f * playerHeight) return 1; // Little, ring, middle, and index fingers
        return 5; // All fingers (excluding thumb and wrist)
    }

    void PlayHapticFeedback(int intensity, int activeMotors)
    {
        int normalizedIntensity = Mathf.Clamp(intensity, (int)minIntensity, (int)maxIntensity);
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

        BhapticsLibrary.PlayMotors((int)PositionType.GloveL, motorValues, durationMillis);
        //BhapticsLibrary.PlayMotors((int)PositionType.GloveR, motorValues, durationMillis);
    }
}


/* 
// NORMALIZED INTENSITY NOT WORKING

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bhaptics.SDK2;

public class HapticsController : MonoBehaviour
{
    public GameObject ball; // Reference to the ball
    public GameObject player; // Reference to the player (OpenXR rig)
    public float tableHeight = 0.232f; // Height of the table surface

    // Distance range for haptic intensity
    public float minDistance = 0.5f;
    public float maxDistance = 1.7f;

    // Minimum and maximum haptic intensity
    public int minIntensity = 1;
    public int maxIntensity = 100;

    // Duration of the haptic feedback in milliseconds
    public int durationMillis = 250;

    void Awake()
    {
        BhapticsLibrary.Play(BhapticsEvent.GLOVE_CHECK);

        player = GameObject.FindGameObjectWithTag("Player");
        ball = GameObject.FindGameObjectWithTag("sceneBall");  // Ensure you have a "Ball" tag assigned to your ball GameObject

        if (player == null || ball == null)
        {
            Debug.LogError("Necessary game objects not found. Please ensure your objects are correctly tagged.");
            this.enabled = false; // Disable the script to prevent further errors if objects are not found
        }
    }


    //help me what is
    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between the player and the ball
        float distance = Vector3.Distance(player.transform.position, ball.transform.position);

        // Calculate the haptic intensity based on the distance
        int intensity = CalculateHapticIntensity(distance);

        // Calculate the number of active motors based on the ball's height
        int activeMotors = CalculateActiveMotors(ball.transform.position.y - tableHeight);

        // Play the haptic feedback with the calculated intensity and active motors
        PlayHapticFeedback(intensity, activeMotors);
    }

    int CalculateHapticIntensity(float distance)
    {
        // Ensure maxDistance is properly defined and greater than minDistance
        if (maxDistance <= minDistance)
        {
            Debug.LogError("maxDistance must be greater than minDistance!");
            return minIntensity;
        }

        // Check if the distance is within the effective range
        if (distance <= maxDistance && distance >= minDistance)
        {
            // Normalize the distance within the range [minDistance, maxDistance]
            float normalizedDistance = (distance - minDistance) / (maxDistance - minDistance);

            // Debug output to check values
            Debug.Log("Normalized Distance: " + normalizedDistance);
            Debug.Log("Distance: " + distance + ", Min Distance: " + minDistance + ", Max Distance: " + maxDistance);

            // Interpolate intensity such that it decreases with increasing distance
            float intensity = Mathf.Lerp(maxIntensity, minIntensity, normalizedDistance);
            Debug.Log("Calculated Intensity: " + intensity);
            return Mathf.RoundToInt(intensity);
        }
        else if (distance < minDistance)
        {
            // If the object is closer than the minimum distance, return maximum intensity
            return maxIntensity;
        }
        else
        {
            // If the object is farther than the maximum distance, return minimum intensity
            return minIntensity;
        }
        // If the distance is within the min and max range
        // if (distance <= maxDistance && distance >= minDistance)
        // {
        //     // Map the distance to the intensity range
        //     float normalizedDistance = (distance - minDistance) / (maxDistance - minDistance);
        //     float intensity = Mathf.Lerp(maxIntensity, minIntensity, normalizedDistance);
        //     return Mathf.RoundToInt(intensity);
        // }
        // else if (distance < minDistance)
        // {
        //     return Mathf.RoundToInt(maxIntensity);
        // }
        // else
        // {
        //     return Mathf.RoundToInt(minIntensity);
        // }
    }

    int CalculateActiveMotors(float height)
    {
        // Determine the number of active motors (fingers) based on the height of the ball
        float playerHeight = player.transform.position.y - tableHeight;
        if (height <= 0.2f * playerHeight) return 1; // Only little finger
        if (height <= 0.4f * playerHeight) return 2; // Little finger and ring finger
        if (height <= 0.6f * playerHeight) return 3; // Little, ring, and middle fingers
        if (height <= 0.8f * playerHeight) return 4; // Little, ring, middle, and index fingers
        return 5; // All fingers (excluding thumb and wrist)
    }

    void PlayHapticFeedback(int intensity, int activeMotors)
    {
        // Normalize the intensity to a range of 1 to 100
        int normalizedIntensity = Mathf.Clamp(intensity, (int)minIntensity, (int)maxIntensity);
        //Debug.Log("Normalized Intensity: " + normalizedIntensity);

        // Create an array for motor intensities. Each TactGlove has 6 motors.
        int[] motorValues = new int[6];

        // Set motor values based on the number of active motors
        for (int i = 0; i < motorValues.Length; i++)
        {
            // Map the number of active motors to the correct fingers
            if (i < activeMotors || i == 4) // include the wrist motor in the intensity calculation
            {
                motorValues[i] = normalizedIntensity;
            }
            else
            {
                motorValues[i] = 0;
            }
        }

        // Play the motors using the Bhaptics library function for both left and right gloves
        BhapticsLibrary.PlayMotors((int)PositionType.GloveL, motorValues, durationMillis);
        BhapticsLibrary.PlayMotors((int)PositionType.GloveR, motorValues, durationMillis);
    }
}

*/





/*

// working haptics script but does not work as required
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bhaptics.SDK2;

public class HapticsController : MonoBehaviour
{
    [SerializeField] public GameObject ball; // Reference to the ball
    public GameObject player; // Reference to the player (OpenXR rig)
    public float tableHeight = 0.0f; // Height of the table surface

    // Maximum distance at which haptics will be felt at full intensity
    public float maxDistance = 10.0f;

    // Minimum and maximum haptic intensity
    public int minIntensity = 1;
    public int maxIntensity = 2;

    // Duration of the haptic feedback in milliseconds
    public int durationMillis = 250;

    void Awake()
    {
        BhapticsLibrary.Play(BhapticsEvent.GLOVE_CHECK);

        player = GameObject.FindGameObjectWithTag("Player");
        ball = GameObject.FindGameObjectWithTag("Ball");  // Ensure you have a "Ball" tag assigned to your ball GameObject

        if (player == null || ball == null)
        {
            Debug.LogError("Necessary game objects not found. Please ensure your objects are correctly tagged.");
            this.enabled = false; // Disable the script to prevent further errors if objects are not found
        }
    }


    void Update()
    {
        // Calculate the distance between the player and the ball
        float distance = Vector3.Distance(player.transform.position, ball.transform.position);

        // Calculate the haptic intensity based on the distance
        int intensity = CalculateHapticIntensity(distance);

        // Calculate the number of active motors based on the ball's height
        int activeMotors = CalculateActiveMotors(ball.transform.position.y - tableHeight);

        // Play the haptic feedback with the calculated intensity and active motors
        PlayHapticFeedback(intensity, activeMotors);
    }

    int CalculateHapticIntensity(float distance)
    {
        if (distance <= maxDistance)
        {
            float intensity = Mathf.Lerp(maxIntensity, minIntensity, distance / maxDistance);
            return Mathf.RoundToInt(intensity);
        }
        return minIntensity;
    }

    int CalculateActiveMotors(float height)
    {
        if (height <= 0.1f) return 4;
        if (height <= 0.3f) return 3;
        if (height <= 0.5f) return 2;
        if (height <= 0.7f) return 1;
        return 5;
    }

    void PlayHapticFeedback(int intensity, int activeMotors)
    {
        int normalizedIntensity = Mathf.Clamp(intensity, 10, 100);
        int[] motorValues = new int[6];
        for (int i = 0; i < motorValues.Length; i++)
        {
            motorValues[i] = (i < activeMotors) ? normalizedIntensity : 0;
        }

        BhapticsLibrary.PlayMotors((int)PositionType.GloveL, motorValues, durationMillis);
        //BhapticsLibrary.PlayMotors((int)PositionType.GloveR, motorValues, durationMillis);
    }
}

///working script ends here
*/










// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Bhaptics.SDK2;

// public class HapticsController : MonoBehaviour
// {
//     [SerializeField] public GameObject ball; // Reference to the ball
//     [SerializeReference] public GameObject player; // Reference to the player (OpenXR rig)
//     public float tableHeight = 0.0f; // Height of the table surface

//     // Maximum distance at which haptics will be felt at full intensity
//     public float maxDistance = 10.0f;

//     // Minimum and maximum haptic intensity
//     public int minIntensity = 10;
//     public int maxIntensity = 100;

//     // Duration of the haptic feedback in milliseconds
//     public int durationMillis = 250;

//     // Update is called once per frame
//     void Update()
//     {
//         // Calculate the distance between the player and the ball
//         float distance = Vector3.Distance(player.transform.position, ball.transform.position);

//         // Calculate the haptic intensity based on the distance
//         int intensity = CalculateHapticIntensity(distance);

//         // Calculate the number of active motors based on the ball's height
//         int activeMotors = CalculateActiveMotors(ball.transform.position.y - tableHeight);

//         // Play the haptic feedback with the calculated intensity and active motors
//         PlayHapticFeedback(intensity, activeMotors);
//     }

//     int CalculateHapticIntensity(float distance)
//     {
//         // If the ball is within the maximum distance
//         if (distance <= maxDistance)
//         {
//             // Map the distance to the intensity range
//             float intensity = Mathf.Lerp(maxIntensity, minIntensity, distance / maxDistance);
//             return Mathf.RoundToInt(intensity);
//         }
//         else
//         {
//             // If the ball is beyond the maximum distance, set intensity to minimum
//             return minIntensity;
//         }
//     }

//     int CalculateActiveMotors(float height)
//     {
//         // Determine the number of active motors (fingers) based on the height of the ball
//         // Assuming a linear relation for simplicity, you can adjust as needed
//         if (height <= 0.1f) return 1; // Only pinky
//         if (height <= 0.3f) return 2; // Pinky and ring
//         if (height <= 0.5f) return 3; // Pinky, ring, and middle
//         if (height <= 0.7f) return 4; // Pinky, ring, middle, and index
//         return 5; // All fingers
//     }

//     void PlayHapticFeedback(int intensity, int activeMotors)
//     {
//         // Normalize the intensity to a range of 0 to 100
//         int normalizedIntensity = Mathf.Clamp(intensity, 10, 100);

//         // Create an array for motor intensities. Each TactGlove has 6 motors.
//         int[] motorValues = new int[6];
//         for (int i = 0; i < motorValues.Length; i++)
//         {
//             // Set motor values based on the number of active motors
//             motorValues[i] = (i < activeMotors) ? normalizedIntensity : 0;
//         }

//         // Play the motors using the Bhaptics library function for both left and right gloves
//         BhapticsLibrary.PlayMotors((int)PositionType.GloveL, motorValues, durationMillis);
//         BhapticsLibrary.PlayMotors((int)PositionType.GloveR, motorValues, durationMillis);
//     }
// }

/*

///working script for the haptics start function/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bhaptics.SDK2;
using Unity.VisualScripting;

public class HapticsController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BhapticsLibrary.Play(BhapticsEvent.GLOVE_CHECK);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
*/

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bhaptics.SDK2;

public class HapticsController : MonoBehaviour
{
    public Transform player;
    public Transform targetObject;
    private float distanceToTarget;

    // Start is called before the first frame update
    void Start()
    {
        BhapticsLibrary.Play(BhapticsEvent.GLOVE_CHECK);
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(player.position, targetObject.position);
        AdjustHapticFeedback(distanceToTarget);
    }

    void AdjustHapticFeedback(float distance)
    {
        
        // Normalize the distance into a range for intensity calculation
        float normalizedIntensity = Mathf.Clamp01(1 - distance / 10.0f); // Assuming 10 units is the max effective distance
        int motorIntensity = (int)(normalizedIntensity * 100); // Scale normalized intensity to 0-100

        // Define the maximum distance and intensity ranges
        int[] motorsIntensities = new int[6] { 0, 0, 0, 0, 0, motorIntensity }; // Example for a device part with 6 motors


        // Set only one specific motor's intensity based on the distance
        //motorsIntensities[5] = motorIntensity; // Activating the third motor as an example

        // Play the haptic motors with the specified intensities and duration
        BhapticsLibrary.PlayMotors(
            position: (int)Bhaptics.SDK2.PositionType.GloveL, // Change to the appropriate position type
            motors: motorsIntensities,
            durationMillis: 1000 // Set duration to 1000 milliseconds
        );
    }
}
*/