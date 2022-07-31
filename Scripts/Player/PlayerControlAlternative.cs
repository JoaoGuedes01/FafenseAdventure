using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControlAlternative : MonoBehaviour
{
    public ParticleSystem dust;

    [Header("Horizontal Movement")]
    public float moveSpeed = 10f;
    public Vector2 direction;
    public bool FacingRight = true;

    [Header("Vertical Movement")]
    public float jumpSpeed = 15f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;
    public float jumpCharges;
    public float wallSlidingSpeed;
    public bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public Animator animCam;
    public GameObject characterHolder;
    public LayerMask groundLayer;
    public Transform frontCheck;
    public Transform backCheck;
    public GameObject bloodSplash;
    

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float gravity = 1;
    public float fallMultiplier = 5f;

    [Header("Collision")]
    public bool onGround = false;
    public bool isTouchingFront = false;
    public bool isTouchingBack = false;
    public bool wallSliding;
    public float groundLength = 0.6f;
    public Vector3 colliderOffset;

    [Header("Controls Logic")]
    public bool levelFinished = false;
    public bool hasPressedE = false;

    [Header("Game Over Screen")]
    public Animator animBackPanel;
    public Animator animGameOverText;
    public Animator animNaoQueroDizerNada;
    public Animator animRetryButton;
    public Animator animQuitButton;
    public GameObject Holder;


    // Update is called once per frame
    void Update()
    {
        bool wasOnGround = onGround;
        
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer)|| Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, 0.1f, groundLayer);
        isTouchingBack = Physics2D.OverlapCircle(backCheck.position, 0.1f, groundLayer);

        
        
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (levelFinished==true)
        {
            jumpSpeed = 0;
            moveSpeed = 0;
            maxSpeed = 0;
        }
        

        if (onGround==true)
        {
            jumpCharges = 2;
            animator.SetBool("isGrounded",true);
        }
        if (onGround==false)
        {
            animator.SetBool("isGrounded", false);
        }

        if (!wasOnGround && onGround)
        {
            animCam.SetTrigger("Shake");
            StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }

        if ((isTouchingFront == true && onGround == false)||(isTouchingBack == true && onGround == false))
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if (Input.GetButtonDown("Jump") && wallSliding == true)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }

        if (wallJumping==true)
        {

            if (isTouchingBack==true && FacingRight==true)
            {
                rb.velocity = new Vector2(xWallForce * 1, yWallForce);
                StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
                createDust();
                jumpCharges = 2;
            }

            if (isTouchingFront == true && FacingRight == false)
            {
                rb.velocity = new Vector2(xWallForce * 1, yWallForce);
                StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
                createDust();
                jumpCharges = 2;
            }

            if (isTouchingBack == true && FacingRight == false)
            {
                rb.velocity = new Vector2(xWallForce * -1, yWallForce);
                StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
                createDust();
                jumpCharges = 2;
            }

            if (isTouchingFront == true && FacingRight == true)
            {
                rb.velocity = new Vector2(xWallForce * -1, yWallForce);
                StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
                createDust();
                jumpCharges = 2;
            }
        }

    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }

    private void FixedUpdate()
    {
        moveCharacter(direction.x);
        modifyPhysics();
        
        if (jumpTimer > Time.time && onGround)
        {
            Jump();

        }
        else if (jumpTimer > Time.time && !onGround && jumpCharges>0)
        {
            
            Jump();
        }
    }

    void moveCharacter(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed*1.8f);
        if (direction.x!=0)
        {
            animator.SetBool("isRunning",true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if ((horizontal >0 && !FacingRight)||(horizontal<0 && FacingRight))
        {
            Flip();
        }
        if (Mathf.Abs(rb.velocity.x)>maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }

    void Jump()
    {
        createDust();
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0;
        StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
        animator.SetTrigger("isJumping");
        jumpCharges -= 1;
    }

    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);
        if (onGround)
        {
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;
            if (rb.velocity.y<0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if (rb.velocity.y >0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }

    void Flip()
    {
        createDust();
        FacingRight = !FacingRight;
        transform.rotation = Quaternion.Euler(0, FacingRight ? 0 : 180, 0);
    }

    IEnumerator JumpSqueeze(float xSqueeze,float ySqueeze,float seconds)
    {
        
        createDust();
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }
    }

    void createDust()
    {
        dust.Play();
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position+ colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Carroca"))
        {
            levelFinished = true;
        }

        if (collision.CompareTag("Spikes"))
        {
           
            Instantiate(bloodSplash, transform.position, Quaternion.identity);
            StartCoroutine(GameOver());
            Holder.SetActive(false);
        }
    }

    IEnumerator GameOver()
    {
        animBackPanel.SetTrigger("GameOver");
        yield return new WaitForSeconds(0.2f);
        animGameOverText.SetTrigger("GameOver");
        animNaoQueroDizerNada.SetTrigger("GameOver");
        animRetryButton.SetTrigger("GameOver");
        animQuitButton.SetTrigger("GameOver");
        gameObject.SetActive(false);
    }

    public void PressedETrue()
    {
        hasPressedE = true;
    }

    public void PressedEFalse()
    {
        hasPressedE = false;
    }


}

