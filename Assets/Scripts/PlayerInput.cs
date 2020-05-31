using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    //Script references
    [SerializeField]
    internal MovementHandler movementHandler;

    //Component references
    private Animator animator;

    //Input
    PlayerInputAction inputAction;

    //Move
    internal float movementDirection { get; set; }

    void Awake()
    {
        inputAction = new PlayerInputAction();
        inputAction.Player.Move.performed += OnMove;

        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            animator.SetBool("paddleHit", true);
        }
    }

    public void AnimationFinished()
    {
        animator.SetBool("paddleHit", false);
    }

    void OnMove(InputAction.CallbackContext input)
    {
        movementDirection = input.ReadValue<float>();
        movementHandler.SetMovement(movementDirection, 10.0f); 
    }
}
