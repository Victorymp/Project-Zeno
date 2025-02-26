using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public List<Transform> cams; // Changed to a list of Transform objects
    public List<Transform> zoomCams; // Added to store the zoomed in cameras
    public int currentCamIndex = 0; // Added to keep track of the current camera

    public float sensX;
    public float sensY;
    private float xRotation;
    private float yRotation; // Changed to a Transform object

    private bool aiming = false;


    // Start is called before the first frame update
    void Start()
    {
        // Set the initial camera
        transform.position = cams[currentCamIndex].position;
        transform.rotation = cams[currentCamIndex].rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cams[currentCamIndex].position;
    }

    public Quaternion Move(Vector2 lookValue)
    {
        float lookValueX = lookValue.x;
        float lookValueY = lookValue.y;

        xRotation += lookValueX * sensX;   
        yRotation -= lookValueY * sensY; 
        if(aiming)
        {
            yRotation = 0;
        }

        if (yRotation > 90 && !aiming)
        {
            yRotation = 90;
        }
        else if (yRotation < -90 && !aiming)
        {
            yRotation = -90;
        }
        // clamp the x roation while aiming 
        if (xRotation > 45 && aiming)
        {
            xRotation = 45;
        }
        else if (xRotation < -45 && aiming)
        {
            xRotation = -45;
        }
        transform.rotation = Quaternion.Euler(yRotation, xRotation, 0);
        return Quaternion.Euler(0, xRotation, 0);
    }
    
    public void ChangeCamera(int index)
    {
        // disable the current camera
        cams[currentCamIndex].gameObject.SetActive(false);

        // update the current camera index
        currentCamIndex = index;

        // enable the new camera
        cams[currentCamIndex].gameObject.SetActive(true);
    }

    public void SetSensitivity(float sens)
    {
        sensX = sens;
        sensY = sens;
    }

    // on aim move the camera to the new position to look like a zoom
    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            aiming = true;
            // Start the transition to the zoom camera
            StartCoroutine(TransitionToZoomCamera(currentCamIndex, 0.5f));
        }
        else if (context.canceled)
        {
            aiming = false;
            // Start the transition back to the current camera
            StartCoroutine(TransitionToZoomCamera(currentCamIndex, 0.5f));
        }
    }

    // Coroutine to transition between cameras
    IEnumerator TransitionToZoomCamera(int index, float duration)
    {
        // Get the current camera and the zoom camera
        Transform currentCam = cams[index];
        Transform zoomCam = zoomCams[index];

        // Get the initial position and rotation of the current camera
        Vector3 startPos = currentCam.position;
        Quaternion startRot = currentCam.rotation;

        // Get the final position and rotation of the zoom camera
        Vector3 endPos = zoomCam.position;
        Quaternion endRot = zoomCam.rotation;

        // Get the initial position and rotation of the zoom camera
        Vector3 startZoomPos = zoomCam.position;
        Quaternion startZoomRot = zoomCam.rotation;

        // Get the final position and rotation of the current camera
        Vector3 endZoomPos = currentCam.position;
        Quaternion endZoomRot = currentCam.rotation;

        // Get the initial time
        float startTime = Time.time;

        // Loop until the duration is reached
        while (Time.time - startTime < duration)
        {
            // Calculate the lerp value
            float t = (Time.time - startTime) / duration;

            // Lerp the position and rotation of the current camera
            currentCam.position = Vector3.Lerp(startPos, endPos, t);
            currentCam.rotation = Quaternion.Lerp(startRot, endRot, t);

            // Lerp the position and rotation of the zoom camera
            zoomCam.position = Vector3.Lerp(startZoomPos, endZoomPos, t);
            zoomCam.rotation = Quaternion.Lerp(startZoomRot, endZoomRot, t);

            // Wait for the next frame
            yield return null;
        }

        // Set the final position and rotation of the current camera
        currentCam.position = endPos;
        currentCam.rotation = endRot;

        // Set the final position and rotation of the zoom camera
        zoomCam.position = endZoomPos;
        zoomCam.rotation = endZoomRot;
    }
}
