using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ball : MonoBehaviour
{
    public AudioSource tableBounce;
    public AudioSource paddleBounce;
    public AudioSource airSound;

    public XRController leftController;
    public XRController rightController;
    public GameObject player;

    private bool uncollided = true;

    private float soundDelay = 0;

    void Awake()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        tableBounce = audios[0];
        paddleBounce = audios[1];
        airSound = audios[2];
        soundDelay = 0;
    }

    void Update()
    {
        if (transform.position.y > -100)
        {
            soundDelay -= Time.deltaTime;

            if (true) // This condition seems redundant. You might want to replace it with the appropriate condition.
            {
                leftController.SendHapticImpulse(0.1f, 50f);
                rightController.SendHapticImpulse(0.1f, 50f);
            }

            if (soundDelay <= 0)
            {
                if (airSound != null)
                {
                    airSound.Play();
                }
                soundDelay = Vector3.Distance(player.transform.position, transform.position) * 0.1f;
            }

            if (soundDelay > 0.3f)
            {
                soundDelay = 0.3f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }

        if (uncollided)
        {
            if (collision.gameObject.layer == 6)
            {
                if (tableBounce != null)
                {
                    tableBounce.Play();
                }
            }
            if (collision.gameObject.layer == 7)
            {
                if (paddleBounce != null)
                {
                    paddleBounce.Play();
                }
            }
        }
    }
}