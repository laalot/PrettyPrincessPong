using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    //Script references
    [SerializeField]
    internal Player player;

    //Component references
    private Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        ApplyMovementForce();
    }

    private void ApplyMovementForce()
    {
        var movementDirection = player.inputHandler.movementDirection;

        if (movementDirection == 1.0f && player.collisionHandler.canMoveUp)
        {
            rb2d.MovePosition(rb2d.position + new Vector2(0.0f, movementDirection * player.paddleSpeed) * Time.fixedDeltaTime);
        }
        if (movementDirection == -1.0f && player.collisionHandler.canMoveDown)
        {
            rb2d.MovePosition(rb2d.position + new Vector2(0.0f, movementDirection * player.paddleSpeed) * Time.fixedDeltaTime);
        }
    }
}
