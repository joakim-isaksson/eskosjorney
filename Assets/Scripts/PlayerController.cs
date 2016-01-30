using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    public bool running = false;
    [HideInInspector]
    public bool grounded = false;
    [HideInInspector]
    public bool freeze = false;

    public float walkingJumpForce;
    public float runningJumpForce;
    public float walkingForce;
    public float runningForce;
    public float maxWalkingSpeed;
    public float maxRunningSpeed;

    private Transform groundCheck;

    private Animator anim;
    private AudioSource audioSource;

    public AudioClip jumpSound;

    void Awake()
    {
        groundCheck = transform.Find("GroundCheck");
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (!freeze)
        {
            if (Input.GetButtonDown("Jump") && grounded) jump = true;
            if (Input.GetButtonDown("Fire1") && grounded) running = true;
            if (Input.GetButtonUp("Fire1")) running = false;
        }
    }

    void FixedUpdate () {

        if (freeze)
        {
            anim.SetBool("idle", true);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            return;
        }

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

        if (jump && !freeze)
        {
            audioSource.PlayOneShot(jumpSound);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

        anim.SetFloat("speedx", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        anim.SetFloat("speedy", GetComponent<Rigidbody2D>().velocity.y);
        anim.SetBool("running", running);
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
