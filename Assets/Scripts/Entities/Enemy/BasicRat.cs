using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRat : EntityBase
{
    //Basic rendering and animation
    Animator anim;
    SpriteRenderer rend;

    Rigidbody2D rb;

    private Transform target;
    private int dir = 1;

    [SerializeField]
    private float turnaroundRate = 3;
    private float turnaroundTime = 0;

    [SerializeField]
    private Transform meleePoint;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private float attackRange = 0.41F;
    [SerializeField]
    private float detectRange = 0.6F;
    [SerializeField]
    private float meleeRate = 2;
    [SerializeField]
    private float nextAttackTime = 0;

    private bool isAttacking = false;

    /** chunks */
    public GameObject bloodParticles, brain;

    public AudioSource deathSound;

    


    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rend = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        PlayerController cont = (PlayerController)FindObjectOfType(typeof(PlayerController));
        target = cont.transform;
    }

    private void FixedUpdate()
    {
        #region UpdateMeleePoint
        float xLoc = Mathf.Abs(meleePoint.localPosition.x);
        if (rend.flipX)
        {
            Debug.Log("Melee Left");
            meleePoint.SetLocalPositionAndRotation(new Vector3(-xLoc, meleePoint.localPosition.y, 0), meleePoint.localRotation);
        }
        else
        {
            meleePoint.SetLocalPositionAndRotation(new Vector3(xLoc, meleePoint.localPosition.y, 0), meleePoint.localRotation);
        }
        #endregion

        if (!isAttacking)
        {
            Move();
        }
        if(Physics2D.OverlapCircleAll(meleePoint.position, detectRange, playerLayer).Length > 0 && !isAttacking)
        {
            rb.velocity = Vector2.zero;
            anim.SetFloat("Speed", 0);
            isAttacking = true;
            nextAttackTime = Time.time + 1 / meleeRate;
        }
        if (isAttacking && Time.time >= nextAttackTime)
        {
            Melee();
            isAttacking = false;
        }

    }

    public override void Move()
    {
            if (Time.time >= turnaroundTime)
            {
                if (target.position.x > this.transform.position.x && dir != 1)
                {
                    dir = 1;
                    turnaroundTime = Time.time + 1f / turnaroundRate;
                }
                else if (target.position.x < this.transform.position.x && dir != -1)
                {
                    dir = -1;
                    turnaroundTime = Time.time + 1f / turnaroundRate;
                }
            }


            //Left or right?
            if (dir < 0)
            {
                rend.flipX = true;
            }
            else if (dir > 0)
            {
                rend.flipX = false;
            }

            anim.SetFloat("Speed", Mathf.Abs(dir));

            Vector2 targetVel = new Vector2(dir * speed * 100 * Time.deltaTime, rb.velocity.y);
            rb.velocity = targetVel;

    }

    private void Melee()
    {

        //Play attack animation
        anim.SetTrigger("Attacking");

        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePoint.position, attackRange, playerLayer);


        //Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EntityBase>().takeDamage(attackPower);
        }
    }
    public override void onDeath()

    {
        //Instantiate(bodyP1, transform.position, Quaternion.identity);
        //Instantiate(bodyP2, transform.position, Quaternion.identity);
        //Instantiate(bodyP3, transform.position, Quaternion.identity);
        //Instantiate(bodyP4, transform.position, Quaternion.identity);
        Instantiate(bloodParticles, transform.position, Quaternion.identity);
        Instantiate(brain, transform.position, Quaternion.identity);

        deathSound.Play();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (meleePoint == null)
        {
            return;
        }

        //Gizmos.DrawWireSphere(meleePoint.position, attackRange);
        Gizmos.DrawWireSphere(meleePoint.position, detectRange);
    }


}
