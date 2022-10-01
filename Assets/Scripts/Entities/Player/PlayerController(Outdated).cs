using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerOut : EntityBase
{

    //Basic rendering and animation
    Animator anim;
    SpriteRenderer rend;

    public int jumpPower = 200;

    Vector2 moveAm;
    Vector2 smoothMoveVel;
    Rigidbody2D rb;

    //The Platformer Sanity Checks (tm)
    bool grounded;
    bool crouching = false;
    public LayerMask groundedMask;
    public Transform groundCheckObj;
    const float groundCheckRad = 0.3F;
    bool jumping = false;
    Vector2 velocityStorage;
    bool justStoppedCan = true;

    //Colliders:
    public Collider2D standardCollision;

    //Enemy Detection
    [SerializeField]
    private LayerMask enemyLayer;

    //Melee Attack
    [SerializeField]
    private Transform meleePoint;
    [SerializeField]
    private float attackRange = 0.5F;
    [SerializeField]
    private float meleeRate = 2;
    [SerializeField]
    private float nextAttackTime = 0;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rend = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        base.FixedUpdate();
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                Melee();
                nextAttackTime = Time.time + 1f / meleeRate;    
            }
        }

    }

    bool CanMove()
    {
        bool can = true;
        if (!can && justStoppedCan)
        {
            velocityStorage = rb.velocity;
            rb.velocity = Vector2.zero;
        }
        return can;
    }

    //checks for ground beneath the player
    void GroundCheck()
    {
        grounded = false;
        //Check if the ground object is colliding with other 2D colliders on the Ground layer
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckObj.position, groundCheckRad, groundedMask);
        if (colliders.Length > 0)
        {
            grounded = true;
            jumping = false;
        }
    }

    bool CanJump()
    {
        if (grounded && !jumping && !crouching)
        {
            return true;
        }
        return false;
    }

    public override void Move()
    {
        int horizRaw = 0;
        if (!CanMove())
        {
            horizRaw = 0;
            rb.velocity = Vector3.zero;
            return;
        }
        justStoppedCan = true;
        velocityStorage = rb.velocity;
        GroundCheck();

        #region Horizontal Movement
        //Checks for right input
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            horizRaw += 1;
            //Checks for left input
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            horizRaw -= 1;
            //Idle
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);

        }
        #endregion

        #region Jump Check
        //Jump input
        if ((Input.GetKey("space") || Input.GetKey("w") || Input.GetKey("up")) && CanJump())
        {
            grounded = false;
            jumping = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0f, jumpPower * 100 * Time.fixedDeltaTime));

        }
        #endregion

        //Left or right?
        if (horizRaw < 0)
        {
            rend.flipX = true;
        }
        else if (horizRaw > 0)
        {
            rend.flipX = false;
        }

        anim.SetFloat("Speed", Mathf.Abs(horizRaw));

        Vector2 targetVel = new Vector2(horizRaw * speed * 100 * Time.deltaTime, rb.velocity.y);
        rb.velocity = targetVel;
    }

    private void Melee()
    {
        float xLoc = Mathf.Abs(meleePoint.localPosition.x);
        if (rend.flipX)
        {
            Debug.Log("Melee Left");
            meleePoint.SetLocalPositionAndRotation(new Vector3(-xLoc, meleePoint.localPosition.y, 0), meleePoint.localRotation);
        } else
        {
            meleePoint.SetLocalPositionAndRotation(new Vector3(xLoc, meleePoint.localPosition.y, 0), meleePoint.localRotation);
        }


        //Play attack animation
        anim.SetTrigger("Attacking");

        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePoint.position, attackRange, enemyLayer);

        
        //Damage enemies
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<EntityBase>().takeDamage(attackPower);
            Debug.Log("Hit enemy");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(meleePoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(meleePoint.position, attackRange);
    }

    public override void onDeath()
    {
        
    }

}
