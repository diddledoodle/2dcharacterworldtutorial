using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotControllerScript : MonoBehaviour {

    public float maxSpeed = 10f;
    bool facingRight = true;
    Rigidbody2D rigidbody2D;
    Animator anim;

    bool Isgrounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 700;

	// Use this for initialization
	void Start ()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Isgrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", Isgrounded);

        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);


        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("MovementSpeed", Mathf.Abs(move));

        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

        if (move > 0 &&!facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
	}

    private void Update()
    {
        Jump();
    }

    private void Jump()
    {
        if (Isgrounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            rigidbody2D.AddForce(new Vector2(0, jumpForce));
            anim.SetTrigger("Jump");
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
