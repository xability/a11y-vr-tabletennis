using System.Collections.Generic;
using UnityEngine;
using VRTableTennis.Core;

namespace VRTableTennis.Gameplay
{
    /// <summary>
    /// Handles automatic ball tossing for the table tennis game
    /// </summary>
    public class Tosser : MonoBehaviour
    {
        [Header("Ball Settings")]
        [SerializeField] private GameObject ball;
        [SerializeField] private int reload = 4;
        public float throwStrength = 100;

        [Header("Timing")]
        private float time;
        private float autoStartTime = 12f;
        private bool autoStartTriggered = false;

        [Header("Shot Positions")]
        private List<Vector3> shotspos = new List<Vector3>();
        private List<Vector3> shotsrot = new List<Vector3>();

        [Header("State")]
        private GameObject lastInstantiatedBall = null;
        private bool canLaunch = false;
        private GameObject rightController;

        void Awake()
        {
            InitializeShotPositions();
            InitializeController();
        }

        void Start()
        {
            time = reload;
        }

        void Update()
        {
            UpdateLaunchState();
            UpdateTimer();
            CheckForToss();
        }

        private void InitializeShotPositions()
        {
            shotspos.Add(new Vector3(0.004889412f, 0.74f, 1.95f));
            shotsrot.Add(new Vector3(0f, 180f, 0f));
        }

        private void InitializeController()
        {
            rightController = GameObject.FindGameObjectWithTag("RightController");
            if (rightController == null)
            {
                Debug.LogError("RightController not found. Make sure you have tagged your right hand controller appropriately.");
            }
        }

        private void UpdateLaunchState()
        {
            canLaunch = IsPaddleInteractedWith();
        }

        private void UpdateTimer()
        {
            if (canLaunch || autoStartTriggered)
            {
                time -= Time.deltaTime;
            }
            else
            {
                autoStartTime -= Time.deltaTime;
                if (autoStartTime <= 0)
                {
                    autoStartTriggered = true;
                }
            }
        }

        private void CheckForToss()
        {
            if (time <= 0 && (canLaunch || autoStartTriggered))
            {
                CleanupLastBall();
                TossObject();
                time = reload;
            }
        }

        private void CleanupLastBall()
        {
            if (lastInstantiatedBall != null)
            {
                Destroy(lastInstantiatedBall);
            }
        }

        private void TossObject()
        {
            int rand = Random.Range(0, shotspos.Count);
            transform.position = shotspos[rand];
            transform.eulerAngles = shotsrot[rand];

            lastInstantiatedBall = Instantiate(ball, transform.position, transform.rotation);
            lastInstantiatedBall.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * throwStrength);
        }

        private bool IsPaddleInteractedWith()
        {
            return (rightController != null && rightController.transform.childCount > 0);
        }
    }
} 