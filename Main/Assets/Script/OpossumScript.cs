using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumScript : Enemy
{

    [SerializeField] private float right;
    [SerializeField] private float left;
    [SerializeField] private float run;
    [SerializeField] private float jump;
    [SerializeField] private LayerMask ground;


    
    private Collider2D collider;

    private bool facingLeft = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void MoveOp()
    {
        if(facingLeft)
        {
            if(transform.position.x > left)
        {
                if(transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                if(collider.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-run, jump);
                }
        }
        else
        {
            facingLeft = false;
        }
        }
        else
        {
            if(transform.position.x < right)
        {
                if(transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                if(collider.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(run, jump);
                }
        }
        else
        {
            facingLeft = true;
        }
        }

    }
}
