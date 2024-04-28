using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScript : MonoBehaviour
{
    public static float volumeLevel;

    Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolumeSliderChanged()
    {
        volumeLevel = volumeSlider.value;
    }
}
