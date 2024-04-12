using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class pointerHandler : MonoBehaviour
{
    [SerializedField] private Window_waypoint waypointer;
    void Start()
    {
        waypointer.Show(new vector3(200 , 45));

        FunctionUpdater.Create(() => {
            switch (state) {
                case 0:
                    if(Vector3.Distance(Camera.main.transform.position . new Vector3(200 , 45) < 50) {
                        waypointer.Show(new Vector3(200 . -100));
                        state = 1;
                    })
            }
        });
    }

    
}
