using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class Ball : MonoBehaviour
{
    public AudioSource tableBounce;
    public AudioSource paddleBounce;
    public AudioSource airSound;

    public GameObject player;
    public XRBaseInteractor leftHand;
    public XRBaseInteractor rightHand;
    private bool uncollided = true;

    private float soundDelay = 0; //how long left before the airsound next plays.

    // Start is called before the first frame update
    void Start()
    {
       
    }

    void Awake()
    {
        AudioSource[] aud; 
        aud = GetComponents<AudioSource>(); //Gets audiosource components from unity inspector in order from top to bottom
        // below are the 3 sounds I used.
        tableBounce = aud[0]; //when the ball bounces off the table
        paddleBounce = aud[1]; // when the ball bounces off the paddle
        airSound = aud[2]; //this one plays periodically based on the balls position to the player's head
        soundDelay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > -100) // the original ball object spawns at -100, so this is just to make sure only the clones (the balls which get tossed) play sounds/haptics
        {
            soundDelay -= Time.deltaTime;
        
            /* //haptics
            if (true)
            {
                leftHand.SendHapticImpulse(0.1f, 50f, 1 - Mathf.Pow(Vector3.Distance(player.transform.position, transform.position), 2)*0.1f); //The long part is just a formula for the intensity of the haptics
                rightHand.SendHapticImpulse(0.1f, 50f, 1f - Mathf.Pow(Vector3.Distance(player.transform.position, transform.position), 2)*0.1f);
            }
            */
            if (soundDelay <= 0)
            {
                if ( airSound != null )
			    {
                    //airSound.volume = 2-Vector3.Distance(player.transform.position, transform.position); //changes volume based on distance from player
				    airSound.Play();
			    }
                 soundDelay = Vector3.Distance(player.transform.position, transform.position)*0.1f;
            }
            if (soundDelay > 0.3f) //there was a bug where sound delay starts off really long, so this is just to make sure that doesnt happen
            {
                soundDelay = 0.3f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3) //ball gets destroyed when it touches the floor right now.
        {
            Destroy(gameObject);
        }
       // if(uncollided){
            if (collision.gameObject.layer == 6)
            {
                //GameObject.GetComponent<>
                if ( tableBounce != null )
			    {
                    //tableBounce.volume = 2-Vector3.Distance(player.transform.position, transform.position); //changes volume based on distance from player
				    tableBounce.Play();
                    //uncollided = false;
			    }
            }
            if (collision.gameObject.layer == 7)
            {
                if ( paddleBounce != null )
			    {
                    //paddleBounce.volume = 2-Vector3.Distance(player.transform.position, transform.position); //changes volume based on distance from player
				    paddleBounce.Play();
                    //uncollided = false;
			    }
            }
       // }
    }

    private void OnTriggerExit()
    {

    }
}

/*
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
                if (audioSourceSetup != null && audioSourceSetup.airSound != null)
                {
                    audioSourceSetup.airSound.Play();
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
                if ( audioSourceSetup != null && audioSourceSetup.tableBounce != null)
                {
                    audioSourceSetup.paddleBounce.Play();
                }
            }
            if (collision.gameObject.layer == 7)
            {
                if (paddleBounce != null && audioSourceSetup.paddleBounce != null)
                {
                    audioSourceSetup.paddleBounce.Play();
                }
            }
        }
    }
}
*/