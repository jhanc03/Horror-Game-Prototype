using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public static float volumeLevel;
    public static bool jumpscare;

    public Slider volumeSlider;
    public Toggle jumpscareToggle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        volumeLevel = volumeSlider.value;
        jumpscare = jumpscareToggle.isOn;
        SceneManager.LoadScene(1);
    }
}
