using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Script references
    [SerializeField]
    internal InputHandler inputHandler;

    [SerializeField]
    internal MovementHandler movementHandler;

    [SerializeField]
    internal CollisionHandler collisionHandler;

    //Component references
    internal Rigidbody2D rb2d;

    //Attributes
    [SerializeField]
    public float paddleSpeed;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
    