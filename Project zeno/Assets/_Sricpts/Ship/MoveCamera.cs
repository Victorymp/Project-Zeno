using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
    public Transform cam;

    public ArrayList cameras;

    public float sensX;
    public float sensY;
    private float xRotation;
    private float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.position;
        if (Input.GetKeyDown(KeyCode.C))
        {
            int index = cameras.IndexOf(cam);
            index++;
            if (index >= cameras.Count)
            {
                index = 0;
            }
            cam = (Transform)cameras[index];
        }
    }

    public void MoveCameraTo(Transform newCam)
    {
        cam = newCam;
    }

    public void Move(Vector2 lookValue)
    {
        xRotation -= lookValue.y * sensY;
        yRotation += lookValue.x * sensX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    public void ChangeCamera(int index)
    {
        cam = (Transform)cameras[index];
    }
}
