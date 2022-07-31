using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float moveSpeed=10f;

    [Header("Components")]
    private Rigidbody2D rb;

    public float jumpForce;
    private int extraJumps;
    private float moveInput;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;



    private bool facingRight = true;

    private bool isGrounded;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        //Controls the Running animations
        moveInput = Input.GetAxis("Horizontal");
        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        //Moves the character
        moveCharacter(moveInput);
        modifyPhysics();

        //Flips the Sprite according to player input
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }
    void moveCharacter(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }

    void modifyPhysics()
    {
        bool changingDirections = (moveInput > 0 && rb.velocity.x < 0) || (moveInput < 0 && rb.velocity.x > 0);
        if (Mathf.Abs(moveInput)<0.4f || changingDirections)
        {
            rb.drag = linearDrag;
        }
        else
        {
            rb.drag = 0f;
        }
        
    }


    private void Update()
    {
        
        if (isGrounded == true)
        {
            extraJumps = 1;
            anim.SetBool("isGrounded",true);
        }

        if (isGrounded == false)
        {
            anim.SetBool("isGrounded", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("isJumping");
            extraJumps -= 1;
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            isGrounded = false;
        }
    }
}
