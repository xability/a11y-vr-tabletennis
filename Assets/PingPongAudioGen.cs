using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongAudioGen : MonoBehaviour
{
    public float frequency = 440; // Adjust for desired pitch
    public int delaySamples = 512; // Adjust for desired delay (can be 0 for mono delay)
    public float gain = 0.8f; // Adjust for desired volume
    public bool useStereoDelay = true; // Enable stereo delay effect if desired

    private float leftPhase = 0f;
    private float rightPhase = 0f;

    void OnAudioFilterRead(float[] data, int channels)
    {
        // Generate sine wave samples
        for (int i = 0; i < data.Length; i += channels)
        {
            float sampleLeft = Mathf.Sin(2f * Mathf.PI * frequency * leftPhase) * gain;
            float sampleRight = Mathf.Sin(2f * Mathf.PI * frequency * rightPhase) * gain;

            // Apply ping-pong delay (can be mono or stereo)
            if (useStereoDelay && channels == 2)
            {
                data[i] = sampleLeft + data[i + channels - delaySamples];
                data[i + 1] = sampleRight + data[i + channels - delaySamples];
            }
            else
            {
                data[i] = sampleLeft + data[i - delaySamples];
            }

            // Update phase values for next samples
            leftPhase += frequency / AudioSettings.outputSampleRate;
            if (leftPhase > 1f)
            {
                leftPhase -= 1f;
            }

            rightPhase = leftPhase + 0.001f; // Add slight offset for stereo panning
            if (rightPhase > 1f)
            {
                rightPhase -= 1f;
            }
        }
    }
}
