using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private Transform meleePoint;
    private SpriteRenderer rend;
    [SerializeField]
    private LayerMask enemyLayer;
    [SerializeField]
    private float attackRange = 0.5F;
    [SerializeField]
    private int attackPower = 2;
    public float attackRate = 2F;
    public float nextAttackTime { get; set; } = 0F;

    private PlayerAnimation playerAnimation;
    private PlayerDecay playerDecay;

    public AudioSource swipeSound;

    public void Awake()
    {
        this.rend = GetComponent<SpriteRenderer>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerDecay = GetComponent<PlayerDecay>();
    }

    public void Melee()
    {
        float xLoc = Mathf.Abs(meleePoint.localPosition.x);
        if (rend.flipX)
        {
            meleePoint.SetLocalPositionAndRotation(new Vector3(-xLoc, meleePoint.localPosition.y, 0), meleePoint.localRotation);
        }
        else
        {
            meleePoint.SetLocalPositionAndRotation(new Vector3(xLoc, meleePoint.localPosition.y, 0), meleePoint.localRotation);
        }


        //Play attack animation
        playerAnimation.attackAnimation(0);
        swipeSound.Play();

        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePoint.position, attackRange, enemyLayer);


        //Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy == null)
            {
                return;
            }
            var ent = enemy.GetComponent<EntityBase>();
            if(ent != null)
            {
                ent.takeDamage(attackPower / (playerDecay.currentState + 1));
            }
        }
    }
}
