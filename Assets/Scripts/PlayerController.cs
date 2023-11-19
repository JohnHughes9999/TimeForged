using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    // floats
    private float moveSpeed = 10f;
    private float horizontalMovement = 0f;
    private float jumpingPower = 10f;

    // ints

    // rigidbodies
    private Rigidbody2D rb;

    // trail renderers

    // bools
    private bool isGrounded = true;

    // vector2s

    // vector3s

    // serialize fields

    // animators
    private Animator animator;
    #endregion
    #region Awake
    // this is called when the script is initialised, this is called before the start function
    private void Awake()
    {
        // storing the rigidbody component in a variable
        rb = GetComponent<Rigidbody2D>();
        // store animator component in animator variable
        animator = GetComponent<Animator>();
    }
    #endregion
    #region Update
    // update function to gather player input - this function is called every frame
    private void Update()
    {
        // player sideways movement input
        horizontalMovement = Input.GetAxis("Horizontal");
        // player jump input - if space is pressed execute this code
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            // change y value of the vector2 to jumping power
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            // set is grounded to false
            isGrounded = false;
            // jump animation
            animator.SetBool("isJumping", !isGrounded);
        }
        /* player smoother jump input
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            // when space bar is pressed down fully, you jump higher than if you just tap it
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }*/
    }
    #endregion
    #region FixedUpdate
    // fixed update function to deal with player movement and player physics - this function is called at a fixed interval - for example every .02 seconds
    private void FixedUpdate()
    {
        // move player
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        // start walk animation
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        // start jump animation animation
        animator.SetFloat("yVelocity", rb.velocity.y);
    }
    #endregion
    #region Method - OnTriggerEnter2D
    // called when player triggers with component that is set to isTrigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // set is grounded to false because the players capsule collider has collided with the ground
        isGrounded = true;
        // enable jump animation
        animator.SetBool("isJumping", isGrounded == false);
    }
    #endregion
}