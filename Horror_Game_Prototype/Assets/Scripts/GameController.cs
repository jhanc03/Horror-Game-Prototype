using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    CameraManager cameraManager;

    //Doors
    public Transform lDoor, rDoor;
    bool lDoorClosed, rDoorClosed
        ,lDoorClosing, lDoorOpening, rDoorClosing, rDoorOpening;
    const float doorRotSpeed = 63.41f;

    // Start is called before the first frame update
    void Start()
    {
        cameraManager = GetComponent<CameraManager>();

        lDoorClosed = false;
        rDoorClosed = false;
        lDoorClosing = false;
        lDoorOpening = false;
        rDoorClosing = false;
        rDoorOpening = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Doors
        //https://docs.unity3d.com/ScriptReference/Transform.Rotate.html
        if (lDoorClosing)
        {
            lDoor.Rotate(Vector3.up, doorRotSpeed * Time.deltaTime);
            if (lDoor.localEulerAngles.y > 0.0f && lDoor.localEulerAngles.y < 3.0f)
            {
                lDoor.localEulerAngles = Vector3.zero;
                lDoorClosing = false;
                lDoorClosed = true;
            }
        }
        else if (lDoorOpening)
        {
            lDoor.Rotate(Vector3.up, -(doorRotSpeed * Time.deltaTime));
            if (lDoor.localEulerAngles.y < 296.59f && lDoor.localEulerAngles.y > 293.59f)
            {
                lDoor.localEulerAngles = new Vector3(0, 296.59f, 0);
                lDoorOpening = false;
                lDoorClosed = false;
            }
        }
        if (rDoorClosing)
        {
            rDoor.Rotate(Vector3.up, -(doorRotSpeed * Time.deltaTime));
            if (rDoor.localEulerAngles.y > 0.0f && rDoor.localEulerAngles.y < 3.0f)
            {
                rDoor.localEulerAngles = Vector3.zero;
                rDoorClosing = false;
                rDoorClosed = true;
            }
        }
        else if (rDoorOpening)
        {
            rDoor.Rotate(Vector3.up, doorRotSpeed * Time.deltaTime);
            if (rDoor.localEulerAngles.y > 63.41f && rDoor.localEulerAngles.y < 66.41f)
            {
                rDoor.localEulerAngles = new Vector3(0, 63.41f, 0);
                rDoorOpening = false;
                rDoorClosed = false;
            }
        }

    }



    //Controls
    public void ToggleCameras()
    {
        cameraManager.ToggleCameras();
    }

    public void ToggleLeftDoor()
    {
        if (!(lDoorClosing || lDoorOpening))
        {
            if (lDoorClosed)
            {
                lDoorOpening = true;
            }
            else
            {
                lDoorClosing = true;
            }
        }
    }
    public void ToggleRightDoor()
    {
        if (!(rDoorClosing || rDoorOpening))
        {
            if (rDoorClosed)
            {
                rDoorOpening = true;
            }
            else
            {
                rDoorClosing = true;
            }
        }
    }
}
