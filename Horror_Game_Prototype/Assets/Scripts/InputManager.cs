using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    GameController gameController;

    public PlayerInputActions playerControls;
    private InputAction toggleCameras, toggleLeftDoor, toggleRightDoor, toggleLDoorLight, toggleRDoorLight, quit;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        toggleCameras = playerControls.Controls.ToggleCameras;
        toggleCameras.Enable();
        toggleCameras.performed += ToggleCameras;

        toggleLeftDoor = playerControls.Controls.ToggleLeftDoor;
        toggleLeftDoor.Enable();
        toggleLeftDoor.performed += ToggleLeftDoor;

        toggleRightDoor = playerControls.Controls.ToggleRightDoor;
        toggleRightDoor.Enable();
        toggleRightDoor.performed += ToggleRightDoor;

        toggleLDoorLight = playerControls.Controls.ToggleLDoorLight;
        toggleLDoorLight.Enable();
        toggleLDoorLight.performed += ToggleLDoorLight;

        toggleRDoorLight = playerControls.Controls.ToggleRDoorLight;
        toggleRDoorLight.Enable();
        toggleRDoorLight.performed += ToggleRDoorLight;

        quit = playerControls.Controls.Quit;
        quit.Enable();
        quit.performed += Quit;
    }

    private void OnDisable()
    {
        toggleCameras.Disable();
        toggleLeftDoor.Disable();
        toggleRightDoor.Disable();
        toggleLDoorLight.Disable();
        toggleRDoorLight.Disable();
        quit.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameController = GetComponent<GameController>();
    }

    private void ToggleCameras(InputAction.CallbackContext context)
    {
        gameController.ToggleCameras();
    }
    private void ToggleLeftDoor(InputAction.CallbackContext context)
    {
        gameController.ToggleLeftDoor();
    }
    private void ToggleRightDoor(InputAction.CallbackContext context)
    {
        gameController.ToggleRightDoor();
    }
    private void ToggleLDoorLight(InputAction.CallbackContext context)
    {
        gameController.ToggleLDoorLight();
    }
    private void ToggleRDoorLight(InputAction.CallbackContext context)
    {
        gameController.ToggleRDoorLight();
    }
    private void Quit(InputAction.CallbackContext context)
    {
        gameController.BackToMenu();
    }
}
