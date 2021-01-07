using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected AudioSource destroy;
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        destroy = GetComponent<AudioSource>();
    }
    public void JumpDown()
    {
        anim.SetTrigger("death");
        rb.velocity = Vector2.zero;
        destroy.Play();
        rb.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}
