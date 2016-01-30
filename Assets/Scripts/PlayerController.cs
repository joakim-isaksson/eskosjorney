using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    public bool running = false;

    public float walkingJumpForce = 1000f;
    public float runningJumpForce = 1300f;
    public float walkingForce = 365f;
    public float runningForce = 800f;
    public float maxWalkingSpeed = 5f;
    public float maxRunningSpeed = 10f;

    private Transform groundCheck;
    private bool grounded = false;

    private Animator anim;

    void Awake()
    {
        groundCheck = transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
    }

    void Start () {

	}

    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded) jump = true;
        if (Input.GetButtonDown("Fire1") && grounded) running = true;
        if (Input.GetButtonUp("Fire1")) running = false;

    }

    void FixedUpdate () {
        float h = Input.GetAxis("Horizontal");

        float force = walkingForce;
        float jumpForce = walkingJumpForce;
        float maxSpeed = maxWalkingSpeed;
        if (running)
        {
            force = runningForce;
            jumpForce = runningJumpForce;
            maxSpeed = maxRunningSpeed;
        }

        GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * force);

        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * force);

        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (jump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
