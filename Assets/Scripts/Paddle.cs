using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Script references
    [SerializeField]
    internal MovementHandler movementHandler;

    [SerializeField]
    internal CollisionHandler collisionHandler;

    //Attributes
    [SerializeField]
    public float paddleSpeed;

}
    