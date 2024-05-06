

//working script for the haptics start function/

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