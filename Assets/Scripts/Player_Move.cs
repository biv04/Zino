using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player_Move : MonoBehaviour
{

   
    public int playerSpeed;
    public int playerJumpPower;
    private float moveX;
    private float moveY;
    public bool allowClick = true;
    public bool skill = false;

    private float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;

    AudioSource footstep;

    public AudioClip sword1;
    public AudioClip sword2;
    public AudioClip damaged1;
    public AudioClip damaged2;
    public AudioClip jump;

    [Tooltip("Only change this if your character is having problems jumping when they shouldn't or not jumping at all.")]
    public float distToGround = 1.0f;
    private bool inControl = true;

    [Tooltip("Everything you jump on should be put in a ground layer. Without this, your player probably* is able to jump infinitely")]
    public LayerMask GroundLayer;

    private float timeBtW;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    private Animator anim;
    public float attackRange;
    public int damage;

    private void Start()
    {
        footstep = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

    }



    // Update is called once per frame
    void Update()
    {

        if (inControl)
        {
            PlayerMove();
            HandleJumpAndFall();
        }

    }

    void PlayerMove()
    {
        //CONTROLS
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            Jump();
        }
       else if (skill && Input.GetButton("Jump") && isJumping==true)
        {
            if (jumpTimeCounter > 0)
            {
                GetComponent<Rigidbody2D>().velocity = (Vector2.up * playerJumpPower);

                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

     

       

        //PLAYER DIRECTION
        if (moveX < 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveX > 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //ANIMATIONS
        if (moveX != 0)
        {
            if (!footstep.isPlaying) footstep.Play();
            anim.SetBool("IsRunning", true);
        }
        else
        {
            footstep.Stop();
            anim.SetBool("IsRunning", false);
        }
        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("IsJump", true);
        }

        else
        {
            anim.SetBool("IsJump", false);

        }



        if (Input.GetMouseButtonDown(0))
        {
          
            anim.SetTrigger("IsAttacking");
            SoundManager.instance.RandomizeSfx(sword1,sword2);
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                /*if(enemiesToDamage[i].GetComponent<Enemy>())
                    enemiesToDamage[i].GetComponent<Enemy>().takeDamage(damage, new Vector2(10,10));
                if (enemiesToDamage[i].GetComponent<enemyFollow>())
                    enemiesToDamage[i].GetComponent<enemyFollow>().TakeDamage(damage);
*/
            }

        }
        else
        {
            Debug.Log("No");
        }




        //PHYSICS
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
      
            //JUMPING CODE
            GetComponent<Rigidbody2D>().velocity=(Vector2.up * playerJumpPower);
            SoundManager.instance.PlaySingle(jump);
        
    }


    void HandleJumpAndFall()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
            {
                GetComponent<Animator>().SetInteger("State", 3);
            }
            else
            {
            }
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distToGround, GroundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Skill"))
        {
            skill = true;
        }
    }

    public void SetControl(bool b)
    {
        inControl = b;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
