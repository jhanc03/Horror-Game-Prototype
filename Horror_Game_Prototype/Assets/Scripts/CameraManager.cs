using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityStandardAssets.Characters.FirstPerson;

public class CameraManager : MonoBehaviour
{
    public GameObject camerasGameObject;
    List<Camera> cameras = new List<Camera>();
    int lastCamera;
    VideoPlayer cameraStatic;
    CanvasGroup cameraUI;

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
        cameraUI = GameObject.FindGameObjectWithTag("UI").GetComponent<CanvasGroup>();
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
            cameraUI.alpha = 1.0f;
            cameraUI.interactable = true;
        }
        else
        {
            cameras[lastCamera].enabled = false;
            cameras[0].enabled = true;
            cameraUI.alpha = 0.0f;
            cameraUI.interactable = false;
        }
    }

    public void PlayerLookAtJumpscare()
    {
        cameras[0].transform.LookAt(new Vector3(-100, 0, 0));
        FirstPersonController player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        player.m_MouseLook.MinimumY = -90.0f;
        player.m_MouseLook.MaximumY = -90.0f;
    }

    //Camera clicks

    public void Camera1Click()
    {
        cameras[1].enabled = true;
        cameras[lastCamera].enabled = false;
        cameraStatic.targetCamera = cameras[1];
        lastCamera = 1;
    }
    public void Camera2Click()
    {
        cameras[2].enabled = true;  
        cameras[lastCamera].enabled = false;
        cameraStatic.targetCamera = cameras[2];
        lastCamera = 2;
    }
    public void Camera3Click()
    {
        cameras[3].enabled = true;
        cameras[lastCamera].enabled = false;
        cameraStatic.targetCamera = cameras[3];
        lastCamera = 3;
    }
    public void Camera4Click()
    {
        cameras[4].enabled = true;
        cameras[lastCamera].enabled = false;
        cameraStatic.targetCamera = cameras[4];
        lastCamera = 4;
    }
    public void Camera5Click()
    {
        cameras[5].enabled = true;
        cameras[lastCamera].enabled = false;
        cameraStatic.targetCamera = cameras[5];
        lastCamera = 5;
    }
    public void Camera6Click()
    {
        cameras[6].enabled = true;
        cameras[lastCamera].enabled = false;
        cameraStatic.targetCamera = cameras[6];
        lastCamera = 6;
    }
}
