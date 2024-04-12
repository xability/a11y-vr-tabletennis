using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_waypoint : MonoBehaviour
{
    // Indicator icon
    public Image img;
    // The target (location, enemy, etc..)
    public Transform target;
    // UI Text to display the distance
    public Text meter;
    // To adjust the position of the icon
    public Vector3 offset;

    private void Update()
    {
        if (target == null)
    {
        // Handle the case where the target is not assigned
        Debug.LogError("Target is not assigned to Window_waypoint.");
        return;
    }
        // Giving limits to the icon so it sticks on the screen
        // Below calculations witht the assumption that the icon anchor point is in the middle
        // Minimum X position: half of the icon width
        float minX = img.GetPixelAdjustedRect().width / 2;
        // Maximum X position: screen width - half of the icon width
        float maxX = Screen.width - minX;

        // Minimum Y position: half of the height
        float minY = img.GetPixelAdjustedRect().height / 2;
        // Maximum Y position: screen height - half of the icon height
        float maxY = Screen.height - minY;

        // Temporary variable to store the converted position from 3D world point to 2D screen point
        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

        // Check if the target is behind us, to only show the icon once the target is in front
        if(Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            // Check if the target is on the left side of the screen
            if(pos.x < Screen.width / 2)
            {
                // Place it on the right (Since it's behind the player, it's the opposite)
                pos.x = maxX;
            }
            else
            {
                // Place it on the left side
                pos.x = minX;
            }
        }

        // Limit the X and Y positions
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Update the marker's position
        img.transform.position = pos;
        // Change the meter text to the distance with the meter unit 'm'
        meter.text = ((int)Vector3.Distance(target.position, transform.position)).ToString() + "m";
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Window_waypoint : MonoBehaviour {

    [SerializeField] private Camera uiCamera;
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private Sprite crossSprite;

    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;
    private Image pointerImage;

    private void Awake() {
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
        pointerImage = transform.Find("Pointer").GetComponent<Image>();

        Hide();
    }

    private void Update() {
        float borderSize = 100f;
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;
        Debug.Log(isOffScreen + " " + targetPositionScreenPoint);

        if (isOffScreen) {
            RotatePointerTowardsTargetPosition();

            pointerImage.sprite = arrowSprite;
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.y = Screen.height - borderSize;

            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        } else {
            pointerImage.sprite = crossSprite;
            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);

            pointerRectTransform.localEulerAngles = Vector3.zero;
        }
    }

    private void RotatePointerTowardsTargetPosition() {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show(Vector3 targetPosition) {
        gameObject.SetActive(true);
        this.targetPosition = targetPosition;
    }
}
*/