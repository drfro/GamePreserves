using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour {

    public Rigidbody2D rb2D;               //The Rigidbody2D component attached to this object.
    public BoxCollider2D bc2D;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public LayerMask blockingLayer;
    public float speed = 1;             //Floating point variable to store the player's movement speed.


    // Use this for initialization
    public virtual void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        bc2D = gameObject.GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void SetTriggers()
    {

        if (rb2D.velocity.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb2D.velocity.x > 0)
        {
            spriteRenderer.flipX = true;
        }
    }

}
