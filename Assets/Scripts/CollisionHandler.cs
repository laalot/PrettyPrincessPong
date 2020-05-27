using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    //Script references
    [SerializeField]
    internal Player player;

    internal bool canMoveUp { get; set; }
    internal bool canMoveDown { get; set; }

    private void Awake()
    {
        canMoveUp = true;
        canMoveDown = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            if (collision.GetContact(0).point.y > transform.position.y)
            {
                canMoveUp = false;
            }
            else if (collision.GetContact(0).point.y < transform.position.y)
            {
                canMoveDown = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            canMoveUp = true;
            canMoveDown = true;
        }
    }
}
