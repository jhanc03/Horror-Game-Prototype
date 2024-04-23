using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    GameController gameController;

    public PlayerInputActions playerControls;
    private InputAction toggleCameras, toggleLeftDoor, toggleRightDoor;

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
    private void ToggleLeftDoor(InputAction.CallbackContext context)
    {
        gameController.ToggleLeftDoor();
    }

    private void ToggleRightDoor(InputAction.CallbackContext context)
    {
        gameController.ToggleRightDoor();
    }
}
