using UnityEngine;

public class PaddleInteraction : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the inspector
    public AudioSource beepingSound; // Assign an AudioSource that contains the beeping sound
    public float activationDistance = 3.0f; // Distance at which the sound starts playing

    private bool isHeld = false; // Flag to check if the paddle is picked up

    void Update()
    {
        if (!isHeld)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= activationDistance)
            {
                if (!beepingSound.isPlaying)
                    beepingSound.Play();
            }
            else
            {
                if (beepingSound.isPlaying)
                    beepingSound.Stop();
            }
        }
    }

    // Call this method when the paddle is picked up
    public void OnPaddlePickedUp()
    {
        isHeld = true;
        beepingSound.Stop();
    }
}

/*
using UnityEngine;

public class AudioGuide : MonoBehaviour
{
    public Transform player;
    public Transform paddle;
    public AudioSource audioSource;
    public float audioThresholdDistance = 1.0f; // Minimum distance to start audio cues
    public float cueInterval = 0.5f; // Time between cues in seconds

    private float timeSinceLastCue = 0;

    void Update()
    {
        float distance = Vector3.Distance(player.position, paddle.position);
        Vector3 directionToPaddle = (paddle.position - player.position).normalized;
        float dotProduct = Vector3.Dot(player.forward, directionToPaddle);

        // Check if the player is facing the paddle and within the threshold distance
        if (distance < audioThresholdDistance && dotProduct > 0.5)
        {
            if (timeSinceLastCue >= cueInterval / (distance + 1)) // Increase frequency as they get closer
            {
                audioSource.Play(); // Play a beep or any directional audio cue
                timeSinceLastCue = 0;
            }
            else
            {
                timeSinceLastCue += Time.deltaTime;
            }
        }
    }
}
*/