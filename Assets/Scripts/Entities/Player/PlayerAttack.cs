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

    public void Awake()
    {
        this.rend = GetComponent<SpriteRenderer>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    public void Melee()
    {
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


        //Play attack animation
        playerAnimation.attackAnimation(0);

        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePoint.position, attackRange, enemyLayer);


        //Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EntityBase>().takeDamage(attackPower);
            Debug.Log("Hit enemy");
        }
    }
}
