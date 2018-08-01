using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour {

    private Transform cameraControl; // the actual camera
    private Transform cameraPivot; // the pivot of the camera
    private Vector3 localRotation;
    private Vector3 localPosition;
    private Vector3 forwardMovementDirection;
    private Vector3 rightMovementDirection;

    [SerializeField]
    private float scrollSensitivity = 3f; // the sensitivity of the scroolwheel.
    [SerializeField]
    public float MouseSensitivity = 4f; // the sensitivity of the mouse. (might get changed when we let the user customize the work enviroment himself.
    [SerializeField]
    private float rotationSensitiviy = 1f; //the sensitivity of the rotation
    [SerializeField]
    private float minimumCloseness = -3; // the closest the camera can get to the pivot.
    [SerializeField]
    private float maximumCloseness = -80; // the farthest the camera can go away from the pivot.
    [SerializeField]
    private float orbitDampening = 10f; //camera orbiting speed dampener.
    [SerializeField]
    private float scrollDampening = 10; // scrolling speed dampener.
    [SerializeField]
    private float movementSpeedRatio = 1f; // camera horizontal and vertical movement speed.
    [SerializeField]
    private float lowestMapPoint = 0f; // the lowest on the map the camera can go to avoid going under the scene.

	void Start ()
    {
        cameraControl = transform;
        cameraPivot = transform.parent;
        forwardMovementDirection = new Vector3(cameraPivot.transform.forward.x, cameraPivot.transform.forward.y, cameraPivot.transform.forward.z);
        rightMovementDirection = new Vector3(cameraPivot.transform.right.x, cameraPivot.transform.right.y, cameraPivot.transform.right.z);
	}
	
	void Update ()
    {
		if(Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float scrollAmount = -Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
            scrollAmount *= (cameraControl.transform.localPosition.z * 0.3f);
            scrollAmount += cameraControl.transform.localPosition.z;
            if (scrollAmount <= minimumCloseness && scrollAmount >= maximumCloseness)
                cameraControl.transform.localPosition = new Vector3(cameraControl.transform.localPosition.x, cameraControl.transform.localPosition.y, scrollAmount);
        }

        if(Input.GetMouseButton(1))
        {
            if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                localRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                localRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

                if (localRotation.y < 0f)
                    localRotation.y = 0f;
                else if (localRotation.y > 90f)
                    localRotation.y = 90f;

                Quaternion QT = Quaternion.Euler(localRotation.y, localRotation.x, 0);
                cameraPivot.rotation = Quaternion.Lerp(this.cameraPivot.rotation, QT, orbitDampening);
            }
        }

        if(Input.GetAxis("Horizontal") != 0)
            cameraPivot.transform.Translate(rightMovementDirection * Input.GetAxis("Horizontal") * movementSpeedRatio);

        if (Input.GetAxis("Vertical") != 0)
            cameraPivot.transform.Translate(forwardMovementDirection * Input.GetAxis("Vertical") * movementSpeedRatio);

        if (cameraPivot.position.y < 0)
            cameraPivot.position = new Vector3(cameraPivot.position.x, lowestMapPoint, cameraPivot.position.z);
    }
}
