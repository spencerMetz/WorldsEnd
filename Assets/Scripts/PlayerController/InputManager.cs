
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] MouseLook mouseLook;

    PlayerControls controller;
    PlayerControls.GroundMovementActions groundMovement;

    Vector2 horizontalInput;
    Vector2 mouseInput;
    

    bool isSprintPressed;

    private void Awake()
    {
        Cursor.visible = false;

        controller = new PlayerControls();
        groundMovement = controller.GroundMovement;

        //groundMovement.[action].performed += content => do something
        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();

        groundMovement.Jump.performed += _ => movement.onJumpPressed();

        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        groundMovement.Sprint.started += onSprint;
        groundMovement.Sprint.canceled += onSprint;
    }

    private void onSprint(InputAction.CallbackContext context)
    {
        isSprintPressed = context.ReadValueAsButton();
    }

    private void Update()
    {
        movement.SetInput(horizontalInput);
        mouseLook.SetInput(mouseInput);
    }

    private void OnEnable()
    {
        controller.Enable();
    }

    private void OnDestroy()
    {
        controller.Disable();
    }
}
