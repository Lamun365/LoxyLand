using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anmim;
    private Collider2D call;
    //private TimeManager Time; //reset time


    //numerator
    private enum Stat { idle, run, jump, fall, hurt}
    private Stat stat = Stat.idle;


    //serialize input
    [SerializeField] private LayerMask ground;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource coin;
    [SerializeField] private AudioSource hurtAudio;

    [SerializeField] private float up = 10f;
    [SerializeField] private float side = 3f;
    [SerializeField] private float hurtForce = 10f;
     //live heath


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anmim = GetComponent<Animator>();
        call = GetComponent<Collider2D>();
        PermanentUI.perm.highScore.text = PlayerPrefs.GetInt("highSc", 0).ToString();
        //HealthActive(); //heath live enable
        LevelActiveUI(); //avtivate level ui
        //Time = FindObjectOfType<TimeManager>(); //reset time when die

    }
    void Update()
    {
        if( stat != Stat.hurt)
        {
            Movement();
        }
        AnimationState();
        anmim.SetInteger("stat", (int)stat); //animation setup
        LevelUI(); //level ui deactive
    } 
    //OnEnter
    private void OnTriggerEnter2D(Collider2D collision) //text Ui
    {
        if(collision.tag == "Collectable")
        {
            coin.Play();
            Destroy(collision.gameObject);
            PermanentUI.perm.cherries += 1;
            PermanentUI.perm.cherryText.text = PermanentUI.perm.cherries.ToString("0");
            
            //highscore
            if(PermanentUI.perm.cherries > PlayerPrefs.GetInt("highSc", 0))
            {
                PlayerPrefs.SetInt("highSc", PermanentUI.perm.cherries);
                PermanentUI.perm.highScore.text = PermanentUI.perm.cherries.ToString();
            }
        }
        if(collision.gameObject.tag == "PowerUp") //powerup
        {
            Destroy(collision.gameObject);
            up = up + 1;
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(ResetPower());

        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if(stat == Stat.fall)
            {
                //Destroy(other.gameObject);
                enemy.JumpDown();
                Jump();
            }

            else
            {
                HeathManager(); //heath control
                stat = Stat.hurt;
                if(other.gameObject.transform.position.x > transform.position.x)
                {
                    //eNemy is right side, therefore i should be damaged and move left
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    //enemy is left side, player move right
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
            }
        }
    }

    //computer movement script
    /*
    private void Movement() // computer build
    {
        float hDirection = Input.GetAxis("Horizontal");
        
        if(hDirection < 0) //moving left
        {
            rb.velocity = new Vector2(-side, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            
        }

        else if(hDirection > 0) //moving right
        {
            rb.velocity = new Vector2(side, rb.velocity.y);
            transform.localScale = new Vector2(1,1);
            
        }

        if(Input.GetButtonDown("Jump") && call.IsTouchingLayers(ground)) //jump
        {
            Jump();
        }
    } */

    //mobile movement script
    
    private void Movement() // android build
    {
        float hDirection = CrossPlatformInputManager.GetAxis("Horizontal");
        
        if(hDirection < 0) //moving left
        {
            rb.velocity = new Vector2(-side, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            
        }

        else if(hDirection > 0) //moving right
        {
            rb.velocity = new Vector2(side, rb.velocity.y);
            transform.localScale = new Vector2(1,1);
            
        }

        if(CrossPlatformInputManager.GetButtonDown("Jump") && call.IsTouchingLayers(ground)) //jump
        {
            Jump();
        }
    } 

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, up);
            stat = Stat.jump;
    }

    private void AnimationState()
    {
        if (stat == Stat.jump)
        {
            if (rb.velocity.y < 0.1f)
            {
                stat = Stat.fall;
            }
        }

        else if(stat == Stat.fall)
        {
            if(call.IsTouchingLayers(ground))
            {
                stat = Stat.idle;
            }
        }
        else if (stat == Stat.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                stat = Stat.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            //following right
            stat = Stat.run;

        }

        else
        {
            stat = Stat.idle;
        }
    }
    private void FootStep()
    {
        footstep.Play();
    }

    private void HurtStep()
    {
        hurtAudio.Play();
    }

    private IEnumerator ResetPower() //powerup Wait
    {
        yield return new WaitForSeconds(5);
        up = up - 1;
        GetComponent<SpriteRenderer>().color = Color.white;

    }
    //Deals with Health
    private void HeathManager()
    {
        PermanentUI.perm.health -= 1;
        if(PermanentUI.perm.health <= 0)
        {
            PermanentUI.perm.Reset();
            //add reset time
            //Time.ResetTime();
            SceneManager.LoadScene("Die");
        }
        if(PermanentUI.perm.health == 5)
        {
            //Destroy(PermanentUI.perm.live[0].gameObject);
            PermanentUI.perm.live[0].gameObject.SetActive(true);
            PermanentUI.perm.live[1].gameObject.SetActive(true);
            PermanentUI.perm.live[2].gameObject.SetActive(true);
            PermanentUI.perm.live[3].gameObject.SetActive(true);
            PermanentUI.perm.live[4].gameObject.SetActive(true);


        }
        if(PermanentUI.perm.health == 4)
        {
            //Destroy(PermanentUI.perm.live[0].gameObject);
            PermanentUI.perm.live[0].gameObject.SetActive(false);
            PermanentUI.perm.live[1].gameObject.SetActive(true);
            PermanentUI.perm.live[2].gameObject.SetActive(true);
            PermanentUI.perm.live[3].gameObject.SetActive(true);
            PermanentUI.perm.live[4].gameObject.SetActive(true);


        }
        if(PermanentUI.perm.health == 3)
        {
            //Destroy(PermanentUI.perm.live[1].gameObject);
            PermanentUI.perm.live[0].gameObject.SetActive(false);
            PermanentUI.perm.live[1].gameObject.SetActive(false);
            PermanentUI.perm.live[2].gameObject.SetActive(true);
            PermanentUI.perm.live[3].gameObject.SetActive(true);
            PermanentUI.perm.live[4].gameObject.SetActive(true);

        }
        if(PermanentUI.perm.health == 2)
        {
            //Destroy(PermanentUI.perm.live[2].gameObject);
            PermanentUI.perm.live[0].gameObject.SetActive(false);
            PermanentUI.perm.live[1].gameObject.SetActive(false);
            PermanentUI.perm.live[2].gameObject.SetActive(false);
            PermanentUI.perm.live[3].gameObject.SetActive(true);
            PermanentUI.perm.live[4].gameObject.SetActive(true);

        }
        if(PermanentUI.perm.health == 1)
        {
            //Destroy(PermanentUI.perm.live[3].gameObject);
            PermanentUI.perm.live[0].gameObject.SetActive(false);
            PermanentUI.perm.live[1].gameObject.SetActive(false);
            PermanentUI.perm.live[2].gameObject.SetActive(false);
            PermanentUI.perm.live[3].gameObject.SetActive(false);
            PermanentUI.perm.live[4].gameObject.SetActive(true);

        }
        if(PermanentUI.perm.health == 0)
        {
            //Destroy(PermanentUI.perm.live[4].gameObject);
            PermanentUI.perm.live[0].gameObject.SetActive(false);
            PermanentUI.perm.live[1].gameObject.SetActive(false);
            PermanentUI.perm.live[2].gameObject.SetActive(false);
            PermanentUI.perm.live[3].gameObject.SetActive(false);
            PermanentUI.perm.live[4].gameObject.SetActive(false);

        }
    }
    private void LevelUI() //level number 
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            PermanentUI.perm.levelInput.text = "1";
        }
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            PermanentUI.perm.levelInput.text = "2";
        }

        if(call.IsTouchingLayers(ground))
        {
            PermanentUI.perm.levelTitle.gameObject.SetActive(false); //level board deactive
            PermanentUI.perm.levelText.gameObject.SetActive(false); // level input disable
        }
    }
    private void LevelActiveUI() //just activate level UI
    {
            PermanentUI.perm.levelTitle.gameObject.SetActive(true); //level board active
            PermanentUI.perm.levelText.gameObject.SetActive(true); // level input able
            PermanentUI.perm.completeLevel.gameObject.SetActive(false); //level complete notif
            //PermanentUI.perm.ResumePanel.gameObject.SetActive(false); // resume
    }
    /*private void HealthActive()
    {
        PermanentUI.perm.live[0].gameObject.SetActive(true);
        PermanentUI.perm.live[1].gameObject.SetActive(true);
        PermanentUI.perm.live[2].gameObject.SetActive(true);
        PermanentUI.perm.live[3].gameObject.SetActive(true);
        PermanentUI.perm.live[4].gameObject.SetActive(true);

    } */

    // **button control**

    /*
    public void RightButton() //right
    {
        rb.velocity = new Vector2(side, rb.velocity.y);
        transform.localScale = new Vector2(1,1);
    }
    public void LeftButton() //left
    {
        rb.velocity = new Vector2(-side, rb.velocity.y);
        transform.localScale = new Vector2(-1, 1);
    }

    public void JumpBtton()
    {
        if(call.IsTouchingLayers(ground))
        {
            Jump();
        }
    } */
}

// previous side force 6, gravity scale 3