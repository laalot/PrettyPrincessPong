using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    //Component references
    private Rigidbody2D rb2d;

    private float movementDirection;
    private float movementMagnitude;

    public float movementSpeed = 15f;
    public float movementFactor = 0.01f;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (movementMagnitude > 10.0f)
        {
            movementMagnitude = 10.0f;
        }

        Vector2 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, -8.5f, 8.5f);
        transform.position = pos;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    public void SetMovement(float moveDir, float moveMag)
    {
        this.movementDirection = moveDir;
        this.movementMagnitude = moveMag;
    }

    private void ApplyMovement()
    {
        if (movementDirection == 1.0f)
        {
            rb2d.MovePosition(rb2d.position + new Vector2(0.0f, movementDirection) * (movementSpeed * (movementFactor * movementMagnitude) * Time.fixedDeltaTime));
        }
        else if (movementDirection == -1.0f)
        {
            rb2d.MovePosition(rb2d.position + new Vector2(0.0f, movementDirection) * (movementSpeed * (movementFactor * movementMagnitude) * Time.fixedDeltaTime));
        }
    }
}   
