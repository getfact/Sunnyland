using UnityEngine;

public class PlayerController : MonoBehaviour {
    // serializers
    public float moveSpeed = 10f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpVelocity = 8f;
    public float groundedSkin = 0.05f;
    public LayerMask mask;

    // private var
    private bool isGrounded;
    private bool jumpRequest;
    public static bool facingRight = true;

    // components
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;

    // vectors
    private Vector2 playerSize;
    private Vector2 boxSize;
    private Vector2 moveInput;

    void Awake () {

        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        // player size for raycast grounding
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x, groundedSkin);
    }

    void Update () {

        Animate();
        Gravity();
        CheckInput();
    }

    void FixedUpdate () {

        Move();
        CheckJump();
    }

    private void Animate () {
        // animations
        myAnimator.SetBool("IsGrounded", isGrounded);
        myAnimator.SetFloat("FallSpeed", -myRigidbody.velocity.y);
        myAnimator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
    }

    private void Gravity () {
        // falling
        if (myRigidbody.velocity.y < 0) {

            myRigidbody.gravityScale = fallMultiplier;
        }

        // low jump cut
        else if (myRigidbody.velocity.y > 0 && !Input.GetButton("Jump")) {

            myRigidbody.gravityScale = lowJumpMultiplier;
        }

        // normal gravity
        else {

            myRigidbody.gravityScale = 1f;
        }
    }

    private void CheckInput () {
        // move input
        var horizontal = Input.GetAxisRaw("Horizontal");
        moveInput = new Vector2(horizontal, 0);

        // flip the player
        if (horizontal > 0 && !facingRight)
            Flip ();

        else if (horizontal < 0 && facingRight)
            Flip ();

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded) {

            jumpRequest = true;
        }
    }

    private void Move () {
        // apply move input
        transform.Translate(moveInput * moveSpeed * Time.deltaTime);
    }

    private void CheckJump () {

        if (jumpRequest) {
            // apply jump
            myRigidbody.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);

            jumpRequest = false;
            isGrounded = false;
        }

        else {
            // grounding
            Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 1.5f;
            isGrounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0f, mask) != null);

        }
    }

    private void Flip () {
        // flip the player direction
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
