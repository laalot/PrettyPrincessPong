using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    //Script references
    [SerializeField]
    internal MovementHandler movementHandler;

    //Input
    PlayerInputAction inputAction;

    //Move
    internal float movementDirection { get; set; }

    void Awake()
    {
        inputAction = new PlayerInputAction();
        inputAction.Player.Move.performed += OnMove;
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    void OnMove(InputAction.CallbackContext input)
    {
        movementDirection = input.ReadValue<float>();
        movementHandler.setMovementDirection(movementDirection);
    }
}
