using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    CameraManager cameraManager;

    //Doors
    public Transform lDoor, rDoor;
    bool lDoorClosed, rDoorClosed,
         lDoorClosing, lDoorOpening, rDoorClosing, rDoorOpening;
    const float doorRotSpeed = 63.41f;

    //Monster
    GameObject monster;

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

        monster = GameObject.FindGameObjectWithTag("Monster");
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
            if (rDoor.localEulerAngles.y > 0.0f && rDoor.localEulerAngles.y < 1.0f)
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

		//Monster
		//Positions:
		//Cam 6 = Vector3(-28.653,0,18.11), Vector3(-30.531,0,21.747)
		//Cam 5 = Vector3(-23.52,0,9.87), Vector3(-26.37,0,4.96), Vector3(-23.59,0,-6.74)
		//Cam 4 = Vector3(-30.15,0,16.2), Vector3(-23.552,0,12.069), Vector3(-20.4529991,0,17.2259998)
		//Cam 3 = Vector3(-13.77,0,-8.46), Vector3(-16.044,0,8.186)
		//Cam 2 = Vector3(-11.54,0,6.51), Vector3(-4.413,0,1.126)
		//Cam 1 = Vector3(-11.78,0,-3.18), Vector3(-5.505,0,-8.497)
		//LDoor = Vector3(0.156,0,-2.914)
		//RDoor = Vector3(0.156,0,2.635)
	}



	//Controls
	public void ToggleCameras()
    {
        cameraManager.ToggleCameras();
    }
	//https://docs.unity3d.com/Manual/ProgressiveLightmapper-UVOverlap.html

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
