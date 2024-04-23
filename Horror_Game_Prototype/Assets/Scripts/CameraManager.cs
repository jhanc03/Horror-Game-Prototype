using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class CameraManager : MonoBehaviour
{
    public GameObject camerasGameObject;
    List<Camera> cameras = new List<Camera>();
    int lastCamera;
    VideoPlayer cameraStatic;

    // Start is called before the first frame update
    void Start()
    {
        cameras.Add(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>());
        foreach (Transform transform in camerasGameObject.transform)
        {
            cameras.Add(transform.gameObject.GetComponent<Camera>());
        }
        cameras[1].enabled = false;
        cameras[2].enabled = false;
        cameras[3].enabled = false;
        cameras[4].enabled = false;
        cameras[5].enabled = false;
        cameras[6].enabled = false;
        lastCamera = 6;

        cameraStatic = camerasGameObject.GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleCameras()
    {
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
    //if (lastCamera !=

    public void Camera1Click()
    {
        Debug.Log("Camera 1 clicked.");
        cameras[1].enabled = true;
        cameras[lastCamera].enabled = false;
        cameraStatic.targetCamera = cameras[1];
        lastCamera = 1;
    }
    public void Camera2Click()
    {
        Debug.Log("Camera 2 clicked.");
        cameras[2].enabled = true;  
        cameras[lastCamera].enabled = false;
        cameraStatic.targetCamera = cameras[2];
        lastCamera = 2;
    }
    public void Camera3Click()
    {
        Debug.Log("Camera 3 clicked.");
        cameras[3].enabled = true;
        cameras[lastCamera].enabled = false;
        cameraStatic.targetCamera = cameras[3];
        lastCamera = 3;
    }
    public void Camera4Click()
    {
        Debug.Log("Camera 4 clicked.");
        cameras[4].enabled = true;
        cameras[lastCamera].enabled = false;
        cameraStatic.targetCamera = cameras[4];
        lastCamera = 4;
    }
    public void Camera5Click()
    {
        Debug.Log("Camera 5 clicked.");
        cameras[5].enabled = true;
        cameras[lastCamera].enabled = false;
        cameraStatic.targetCamera = cameras[5];
        lastCamera = 5;
    }
    public void Camera6Click()
    {
        Debug.Log("Camera 6 clicked.");
        cameras[6].enabled = true;
        cameras[lastCamera].enabled = false;
        cameraStatic.targetCamera = cameras[6];
        lastCamera = 6;
    }
}
