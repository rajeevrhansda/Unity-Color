using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    bool Jumping;
    bool facingRight;
    bool isGrounded;
    bool canDoubleJump;

    public float speedX;
    public float jumpSpeedY;
    public float delayBeforeDoubleJump;


    private Rigidbody2D rb;
    private float speed;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        facingRight = true;
        
    }
    
    void Update()
    {
        MovePlayer(speed);

        Flip();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            speed = -speedX;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            speed = 0;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            speed = speedX;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            speed = 0;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && canDoubleJump)
        {
            Jump();
        }

    }

    void MovePlayer(float plyerSpeed)
    {
        rb.velocity = new Vector3(speed, rb.velocity.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            canDoubleJump = false;
            Jumping = false;
        }
    }    

    public void WalkRight()
    {
        speed = speedX;
    }
    public void WalkLeft()
    {
        speed = -speedX;
    }
    public void StopMoving()
    {
        speed = 0;
    }
    public void Jump()
    {
        if (isGrounded)
        {
            isGrounded = false;
            Jumping = true;
            rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY));
            Invoke("EnableDoubleJump", delayBeforeDoubleJump);
        }
        if (canDoubleJump)
        {
            canDoubleJump = false;
            Jumping = true;
            rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY));
        }
    }

    void EnableDoubleJump()
    {
        canDoubleJump = true;
    }

    void Flip()
    {
        if(speed > 0 && !facingRight || speed < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;

        }
    }
}
