using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tosser : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] int reload; //length of time in between each toss
    public int throwStrength; //strength of throws
    private float time;
    public AudioSource launchSound; //sound that plays every time a ball is tossed


    private List<Vector3> shotspos = new List<Vector3>(); // list of positions for the tosser to be at
    private List<Vector3> shotsrot = new List<Vector3>(); // list of rotations for the tosser to have
    // Start is called before the first frame update
    void Awake(){
        shotspos.Add(new Vector3(2f,1.1f,0f)); //make sure to have the position/rotation in the same index in the array for them to match up
        shotsrot.Add(new Vector3(15f,-90f,0f));

        shotspos.Add(new Vector3(2f,1.1f,0.5f));
        shotsrot.Add(new Vector3(15f,-90f,0f));

        shotspos.Add(new Vector3(2f,1.1f,-0.5f));
        shotsrot.Add(new Vector3(15f,-90f,0f));
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            int rand = Random.Range(0, shotspos.Count);
            //random = Mathf.Floor(random);
            transform.position = shotspos[rand];
            transform.eulerAngles = shotsrot[rand];

            time = reload;
            GameObject temp;
            
            temp = Instantiate<GameObject>(ball, transform.position, transform.rotation);
            temp.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * throwStrength);
             if ( launchSound != null )
			{
				launchSound.Play();

			}
           
        }




    }

   
}