using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : Enemy
{
    //serial field
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private float jumpHeight = 14f;
    [SerializeField] private LayerMask ground;
    private Collider2D coll;

    

    private bool facingLeft = true;

    //state animation 
    /*private enum State {idle, jumping, fall}
    private State state = State.idle;*/

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        
    }

    private void Update()
    {
        //anim.SetInteger("State", (int)state);
        //StateManager();

        //jump to fall
        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y < -1)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        
        //fall to idle
        if(anim.GetBool("falling") && coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
        }
    }

    private void Move()
    {
        if(facingLeft)
        {
            if(transform.position.x > leftCap)
            {
                if(transform.localScale.x != 1)
                { //facing right
                    transform.localScale = new Vector3(1, 1, 1);
                }
                //facing left and jump
                if(coll.IsTouchingLayers(ground))
                {
                    //jumping
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    //state = State.jumping;
                    anim.SetBool("jumping", true);
                    
                }
            }
            else
            {
                //move it to right
                facingLeft = false;
            }
        }
        else
        {
            if(transform.position.x < rightCap)
            {
                if(transform.localScale.x != -1)
                { //facing left
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                //facing right and jump
                if(coll.IsTouchingLayers(ground))
                {
                    //jumping
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    //state = State.jumping;
                    anim.SetBool("jumping", true);
                    
                }
            }
            else
            {
                //move it to left
                facingLeft = true;
            }
        }
    }

    
    /*private void StateManager()
    {
        if(state == State.jumping)
        {
            if(Mathf.Abs(rb.velocity.y) < 1f)
            {
                state = State.fall;
            }
        }
        else if(state == State.fall)
        {
            if(Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                state = State.idle;
            }
        }
        else
        {
            state = State.idle;
        }
    }*/

}
