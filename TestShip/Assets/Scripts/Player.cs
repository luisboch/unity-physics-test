using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : PhysicsObject
{
    public static Player PlayerManager { get; private set; }

    private float speed = 7f;

    public float jumpTakeOffSpeed = 7f;

    public Vector2 move;
    public bool rightSide;

    //Player States
    private bool doubleJump;

    void Awake()
    {
        PlayerManager = this;
    }

    protected override void ComputeVelocity()
    {
        move.x = Input.GetAxis("Horizontal");
        MovePlayer();       
    }

    void MovePlayer()
    {

        bool flipSprite = (rightSide ? (move.x > 0.0f) : (move.x < 0.0f));

        if (flipSprite)
        {
            rightSide = !rightSide;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            //            sprite.flipX = !sprite.flipX;
        }
        
        //Checks the Jump Input
        if ((Input.GetKeyDown(KeyCode.UpArrow)) && (grounded))
        {

            velocity.y = jumpTakeOffSpeed;

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }
        targetVelocity = move * speed;
        
    }


    public bool IsGrounded()
    {
        return grounded;
    }


}
