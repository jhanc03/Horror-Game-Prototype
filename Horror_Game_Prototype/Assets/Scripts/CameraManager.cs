using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public GameObject player;
    public GameObject camerasGO;
    List<Camera> cameras = new List<Camera>();
    int lastCamera;

    // Start is called before the first frame update
    void Start()
    {
        cameras.Add(player.GetComponentInChildren<Camera>());
        foreach (Transform transform in camerasGO.transform)
        {
            cameras.Add(transform.gameObject.GetComponent<Camera>());
        }
        cameras[1].enabled = false;
        cameras[2].enabled = false;
        cameras[3].enabled = false;
        cameras[4].enabled = false;
        cameras[5].enabled = false;
        lastCamera = 5;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenCameras(InputAction.CallbackContext context)
    {
        Debug.Log("Yeah");
        if (cameras[0].enabled)
        {
            cameras[0].enabled = false;
            cameras[lastCamera].enabled = true;
        }
        else
        {
            cameras[lastCamera].enabled = false;
            cameras[0].enabled = true;
        }
    }

    //Camera clicks

    public void Camera1Click()
    {
        Debug.Log("Camera 1 clicked.");
        cameras[1].enabled = true;
        cameras[lastCamera].enabled = false;
        lastCamera = 1;
    }
    public void Camera2Click()
    {
        Debug.Log("Camera 2 clicked.");
        cameras[2].enabled = true;  
        cameras[lastCamera].enabled = false;
        lastCamera = 2;
    }
    public void Camera3Click()
    {
        Debug.Log("Camera 3 clicked.");
        cameras[3].enabled = true;
        cameras[lastCamera].enabled = false;
        lastCamera = 3;
    }
    public void Camera4Click()
    {
        Debug.Log("Camera 4 clicked.");
        cameras[4].enabled = true;
        cameras[lastCamera].enabled = false;
        lastCamera = 4;
    }
    public void Camera5Click()
    {
        Debug.Log("Camera 5 clicked.");
        cameras[5].enabled = true;
        cameras[lastCamera].enabled = false;
        lastCamera = 5;
    }
}
