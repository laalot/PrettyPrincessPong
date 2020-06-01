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

    //Wings
    public GameObject topWing;
    public GameObject bottomWing;

    private Animator topWingAnimator;
    private Animator bottomWingAnimator;

    //Input
    PlayerInputAction inputAction;

    //Move
    internal float movementDirection { get; set; }

    void Awake()
    {
        inputAction = new PlayerInputAction();
        inputAction.Player.Move.performed += OnMove;

        animator = GetComponent<Animator>();
        topWingAnimator = topWing.GetComponent<Animator>();
        bottomWingAnimator = bottomWing.GetComponent<Animator>();
    }

    private void Update()
    {
        if (movementHandler.IsMoving())
        {
            topWingAnimator.speed = 1.5f;
            bottomWingAnimator.speed = 1.5f;
        }
        else
        {
            topWingAnimator.speed = 0.5f;
            bottomWingAnimator.speed = 0.5f;
        }
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
