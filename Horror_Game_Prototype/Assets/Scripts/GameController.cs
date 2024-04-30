using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	CameraManager cameraManager;
	MonsterManager monsterManager;
	LightManager lightManager;

	AudioSource officeSfx;
	public AudioClip powerDown, amb1, amb2, amb3, jumpscareStinger, endMusic;
	float ambTimer = 0.0f;
	int lastAmb = -1;
	bool poweredDown = false;

	//Doors
	public Transform lDoor, rDoor;
	bool lDoorClosed, rDoorClosed,
		 lDoorClosing, lDoorOpening, rDoorClosing, rDoorOpening,
		 camerasUp, jumpscareReady, jumpscareSent = false;
	const float doorRotSpeed = 63.41f;

	//Power
	int power;
	float powerDrainRate = 4.4f, powerTimer;
	bool lDoorLightOn, rDoorLightOn;

	float jumpscareTimer = 2.04f;

	float gameTimer = 0.0f, gameEnd = 244f;
	public bool won = false;

	//UI
	public Text powerText, winLossText, timeText;
	public Image blackBackground;
	public Button menuButton;

	// Start is called before the first frame update
	void Start()
	{
		Random.InitState((int)DateTime.Now.Ticks);

        cameraManager = GetComponent<CameraManager>();
		monsterManager = GetComponent<MonsterManager>();
		lightManager = GetComponent<LightManager>();
		officeSfx = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
		
		lDoorClosed = false;
		rDoorClosed = false;
		lDoorClosing = false;
		lDoorOpening = false;
		rDoorClosing = false;
		rDoorOpening = false;

		lDoorLightOn = false;
		rDoorLightOn = false;

		camerasUp = false;
		jumpscareReady = false;
		power = 100;
		powerTimer = 0.0f;

        officeSfx.volume = MainMenuScript.volumeLevel;
    }

    // Update is called once per frame
    void Update()
	{
		gameTimer += Time.deltaTime;
		float completed = gameTimer / gameEnd;
		if (completed < (1.0f / 6.0f))
		{
			timeText.text = "12AM";
        }
		else
		{
			timeText.text = String.Format("{0}AM", (int)(completed * 6));
		}
		if (gameTimer > gameEnd && !jumpscareReady && !won)
		{
            //Win
            //Black screen, mute audio, show text
            blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, 255);
            officeSfx.volume = 0;
            monsterManager.MuteSources();
            winLossText.text = "You win!";
			menuButton.image.color = new Color(menuButton.image.color.r, menuButton.image.color.g, menuButton.image.color.b, 255);
			Text menuButtonText = menuButton.GetComponentInChildren<Text>();
			menuButtonText.color = new Color(menuButtonText.color.r, menuButtonText.color.g, menuButtonText.color.b, 255);
			won = true;
			officeSfx.PlayOneShot(endMusic, 0.4f);
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
					officeSfx.PlayOneShot(jumpscareStinger, 0.48f);

					jumpscareSent = true;
				}
				else
				{
					jumpscareTimer -= Time.deltaTime;
					if (jumpscareTimer < 0)
					{
						//Lose
						//Black screen, mute audio, show text
						blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, 255);
						officeSfx.volume = 0;
						monsterManager.MuteSources();
						winLossText.text = "You lose!";
                        menuButton.image.color = new Color(menuButton.image.color.r, menuButton.image.color.g, menuButton.image.color.b, 255);
                        Text menuButtonText = menuButton.GetComponentInChildren<Text>();
                        menuButtonText.color = new Color(menuButtonText.color.r, menuButtonText.color.g, menuButtonText.color.b, 255);
						AudioSource.PlayClipAtPoint(endMusic, new Vector3(1.86f, 1.095f, 0), 0.04f);
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

		//Power
		if (!won)
		{
			powerTimer += Time.deltaTime;
		}
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
			lightManager.LightsOff();
        }
		powerText.text = String.Format("Power: {0}%", power);

		//Ambience
		if (!officeSfx.isPlaying)
		{
			ambTimer += Time.deltaTime;
		}
		if (ambTimer > 4.4f)
		{
			switch (Mathf.Floor(Random.Range(0.0f, 3.0f)))
			{
				case 0:
					if (lastAmb != 0)
					{
						officeSfx.PlayOneShot(amb1, 0.04f);
                        ambTimer = 0.0f;
						lastAmb = 0;
                    }
					break;

				case 1:
					if (lastAmb != 1)
					{
						officeSfx.PlayOneShot(amb2, 0.04f);
						ambTimer = 0.0f;
						lastAmb = 1;
					}
                    break;

				case 2:
					if (lastAmb != 2)
					{
						officeSfx.PlayOneShot(amb3, 0.04f);
						ambTimer = 0.0f;
						lastAmb = 2;
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
				powerDrainRate += 0.4f;
			}
			else
			{
				powerDrainRate -= 0.4f;
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
				powerDrainRate += 0.4f;
			}
			else
			{
				lDoorClosing = true;
				powerDrainRate -= 0.4f;
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
                powerDrainRate += 0.4f;
            }
			else
			{
				rDoorClosing = true;
                powerDrainRate -= 0.4f;
            }
		}
	}

	public void ToggleLDoorLight()
	{
        if (!camerasUp && !poweredDown)
        {
			lightManager.ToggleLDoorLight();
			if (lDoorLightOn)
			{
                powerDrainRate += 0.4f;
            }
			else
			{
                powerDrainRate -= 0.4f;
            }
            lDoorLightOn = !lDoorLightOn;
        }
    }
    public void ToggleRDoorLight()
    {
        if (!camerasUp && !poweredDown)
        {
            lightManager.ToggleRDoorLight();
            if (rDoorLightOn)
            {
                powerDrainRate += 0.4f;
            }
            else
            {
                powerDrainRate -= 0.4f;
            }
            rDoorLightOn = !rDoorLightOn;
        }
    }

    public void BackToMenu()
	{
        SceneManager.LoadScene(0);
    }
}
