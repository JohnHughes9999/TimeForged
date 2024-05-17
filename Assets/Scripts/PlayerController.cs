using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    // floats
    private float moveSpeed = 10f;
    private float horizontalMovement = 0f;
    private float jumpingPower = 18f;

    // rigidbodies
    private Rigidbody2D rb = null;

    // animators
    private Animator animator;

    // bools
    private bool isGrounded = true;
    private bool facingRight = true;

    // vector3s
    private Vector3 currentScale = new Vector3(0, 0, 0);
    #endregion
    #region Awake
    // this is called when the script is initialised, this is called before the start function
    private void Awake()
    {
        /* this is where we store components in variables */

        rb = GetComponent<Rigidbody2D>();
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
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // change y value of the vector2 to jumping power
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            // set is grounded to false
            isGrounded = false;
            // jump animation
            animator.SetBool("isJumping", !isGrounded);
        }

        /*if (Input.GetButtonUp("Jump") && rb.velocity.y > 0.0f)
        {
            coyoteTimeCounter = 0.0f;
        }*/

        #region Checking for Flip
        if (horizontalMovement > 0 && !facingRight)
        {
            Flip();
        }

        if (horizontalMovement < 0 && facingRight)
        {
            Flip();
        }
        #endregion
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
    #region Method - Flip
    // method to flip player sprite - should be called in update so that it checks every frame
    private void Flip()
    {
       /* this method is used to flip the player i will call it in update and if the
        player is moving right and facing left as well as when the player is moving left
        but facing right, this way the player should always be facing the right way */

       /* current scale is a vector 3 that includes the current scale of the player, e.g
        x,y & z values. it then changes the x value in current state to *-1 which means it
        flips the character, because 1 times -1 is -1 and -1 times -1 is 1, then we set the
        scale of the player to the new current scale and change the facing right bool to
        the opposite */

        currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }
    #endregion
}