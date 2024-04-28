using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
	public float volumeLevel; // <<<<<<<<<<<<<<<<<<<<<<

	//Lighting https://docs.unity3d.com/Manual/ProgressiveLightmapper-UVOverlap.html

	//Just change volume of all audio sources - combines both

	CameraManager cameraManager;
	MonsterManager monsterManager;

	AudioSource officeSfx;
	public AudioClip powerDown, amb1, amb2, amb3;
	float ambTimer = 0.0f;
	int lastAmb;
	bool poweredDown = false;

	//Doors
	public Transform lDoor, rDoor;
	bool lDoorClosed, rDoorClosed,
		 lDoorClosing, lDoorOpening, rDoorClosing, rDoorOpening,
		 camerasUp, jumpscareReady, jumpscareSent = false;
	const float doorRotSpeed = 63.41f;

	int power;
	float powerDrainRate = 5.0f, powerTimer;

	float jumpscareTimer = 1.0f;

	float gameTimer = 0.0f, gameEnd = 124f;

	// Start is called before the first frame update
	void Start()
	{
		Random.InitState((int)DateTime.Now.Ticks);

        cameraManager = GetComponent<CameraManager>();
		monsterManager = GetComponent<MonsterManager>();
		officeSfx = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
		
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

        officeSfx.volume = volumeLevel;
    }

    // Update is called once per frame
    void Update()
	{
		gameTimer += Time.deltaTime;
		if (gameTimer > gameEnd)
		{
			//Win
		}

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
						//Lose
						Application.Quit();
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
        if (power <= 0 && !poweredDown)
        {
			//Power out
			lDoorClosing = false;
			lDoorOpening = true;
            rDoorClosing = false;
            rDoorOpening = true;
			officeSfx.PlayOneShot(powerDown, 0.8f);
			poweredDown = true;

			//Turn off lights
        }

		//Ambience
		if (!officeSfx.isPlaying)
		{
			ambTimer += Time.deltaTime;
		}
		if (ambTimer > 4.4f)
		{
			switch (Random.Range(0, 3))
			{
				case 0:
					if (lastAmb != 0)
					{
						officeSfx.PlayOneShot(amb1, 0.04f);
                        ambTimer = 0.0f;
                    }
					break;

				case 1:
					if (lastAmb != 1)
					{
						officeSfx.PlayOneShot(amb1, 0.04f);
						ambTimer = 0.0f;
					}
                    break;

				case 2:
					if (lastAmb != 2)
					{
						officeSfx.PlayOneShot(amb1, 0.04f);
						ambTimer = 0.0f;
					}
                    break;
			}
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
		if (!poweredDown)
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
	}

	public void ToggleLeftDoor()
	{
		if (!(lDoorClosing || lDoorOpening) && !camerasUp && !poweredDown)
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
		if (!(rDoorClosing || rDoorOpening) && !camerasUp && !poweredDown)
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
