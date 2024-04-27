using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
	//Lighting https://docs.unity3d.com/Manual/ProgressiveLightmapper-UVOverlap.html

	CameraManager cameraManager;
	MonsterManager monsterManager;

	//Doors
	public Transform lDoor, rDoor;
	bool lDoorClosed, rDoorClosed,
		 lDoorClosing, lDoorOpening, rDoorClosing, rDoorOpening,
		 camerasUp, jumpscareReady, jumpscareSent = false;
	const float doorRotSpeed = 63.41f;

	int power;
	float powerDrainRate = 5.0f, powerTimer;

	float jumpscareTimer = 3.0f;

	// Start is called before the first frame update
	void Start()
	{
		cameraManager = GetComponent<CameraManager>();
		monsterManager = GetComponent<MonsterManager>();
		
		lDoorClosed = false;
		rDoorClosed = false;
		lDoorClosing = false;
		lDoorOpening = false;
		rDoorClosing = false;
		rDoorOpening = false;

		camerasUp = false;
		jumpscareReady = false;
		power = 100;
		powerTimer = 0.0f;
	}

	// Update is called once per frame
	void Update()
	{
		//Jumpscare
		if (jumpscareReady)
		{
			if (!camerasUp)
			{
				//Jumpscare
				if (!jumpscareSent)
				{
					monsterManager.MonsterJumpscare();
					cameraManager.PlayerLookAtJumpscare();

					jumpscareSent = true;
				}
				else
				{
					jumpscareTimer -= Time.deltaTime;
					if (jumpscareTimer < 0)
					{
						//End game
					}
				}
            }
            else
			{
				//Play breathing sfx
				monsterManager.Breathing();
			}
		}
		else
		{
			//Doors
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
		}

		//Power
		powerTimer += Time.deltaTime;
		if (powerTimer >= powerDrainRate)
		{
			power--;
			powerTimer = 0.0f;
		}
	}

	public bool GetLDoorClosed()
	{
		return lDoorClosed;
	}
	public bool GetRDoorClosed()
	{
		return rDoorClosed;
	}

	public void JumpscareReady()
	{
		jumpscareReady = true;
	}

	//Controls
	public void ToggleCameras()
	{
		cameraManager.ToggleCameras();
		if (camerasUp)
		{
			powerDrainRate += 1.0f;
		}
		else
		{
			powerDrainRate -= 1.0f;
		}
		camerasUp = !camerasUp;
	}

	public void ToggleLeftDoor()
	{
		if (!(lDoorClosing || lDoorOpening) && !camerasUp)
		{
			if (lDoorClosed)
			{
				lDoorOpening = true;
				powerDrainRate += 1.0f;
			}
			else
			{
				lDoorClosing = true;
				powerDrainRate -= 1.0f;
			}
		}
	}
	public void ToggleRightDoor()
	{
		if (!(rDoorClosing || rDoorOpening) && !camerasUp)
		{
			if (rDoorClosed)
			{
				rDoorOpening = true;
                powerDrainRate += 1.0f;
            }
			else
			{
				rDoorClosing = true;
                powerDrainRate -= 1.0f;
            }
		}
	}
}
