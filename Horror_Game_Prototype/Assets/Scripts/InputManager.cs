using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    GameController gameController;

    public PlayerInputActions playerControls;

    private InputAction toggleCameras;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        toggleCameras = playerControls.Controls.ToggleCameras;
        toggleCameras.Enable();
        toggleCameras.performed += ToggleCameras;
    }

    private void OnDisable()
    {
        toggleCameras.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameController = GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ToggleCameras(InputAction.CallbackContext context)
    {
        gameController.ToggleCameras();
    }
}
