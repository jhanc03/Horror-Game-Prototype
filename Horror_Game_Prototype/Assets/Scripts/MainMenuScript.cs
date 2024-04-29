using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public static float volumeLevel;
    public static bool jumpscare;
    public static int difficulty;

    AudioSource musicSource;

    public Slider volumeSlider;
    public Toggle jumpscareToggle;

    public void Start()
    {
        difficulty = 5;
        musicSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        musicSource.volume = volumeSlider.value / 0.64f;
    }

    public void StartGame()
    {
        volumeLevel = volumeSlider.value;
        jumpscare = jumpscareToggle.isOn;
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Easy()
    {
        difficulty = 5;
    }
    public void Moderate()
    {
        difficulty = 10;
    }
    public void Hard()
    {
        difficulty = 15;
    }
}
