using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {

    public Transform player;
    Vector3 firstPos;
    bool isAttack;
    bool isRotate;
    public float health;
    float maxHealth;
    float howFar;

    public BossWeapon weapon0;
    public BossWeapon weapon1;
    public BossWeapon weapon2;
    public BossWeapon weapon3;
    public BossWeapon weapon4;
    public BossWeapon weapon5;

    List<BossWeapon> weapons;

    public enum BossActionType
    {
        Idle,
        Moving,
        AvoidingObstacle,
        Patrolling,
        AttackingType0,
        AttackingType1,
        AttackingType2
    }

    private BossActionType eCurState = BossActionType.Idle;


    // Use this for initialization
    void Start () {
        firstPos = transform.position;
        maxHealth = health;
        isAttack = false;
        isRotate = true;
        weapons = new List<BossWeapon> { weapon0, weapon1, weapon2, weapon3, weapon4, weapon5 };
    }
	
	// Update is called once per frame
	void Update () {
        howFar = Vector3.Distance(transform.position, player.position);

        DeactivateByDistance();
        Rotate();
        if (health <= 0)
        {
            Die();
        }



        switch (eCurState)
        {
            case BossActionType.Idle:
                HandleIdleState();
                break;

            //case BossActionType.Moving:
            //    HandleMovingState();
            //    break;

            //case BossActionType.AvoidingObstacle:
            //    HandleAvoidingObstacleState();
            //    break;

            //case BossActionType.Patrolling:
            //    HandlePatrollingState();
            //    break;

            case BossActionType.AttackingType0:
                HandleAttackingType0State();
                break;
            case BossActionType.AttackingType1:
                HandleAttackingType1State();
                break;
        }
    }

    void FixedUpdate()
    {
       
    }

    void HandleIdleState()
    {
        InstaRegenerate();

        if (howFar<50)
        {
            eCurState = BossActionType.AttackingType0;
        }
    }

    void HandleAttackingType0State()
    {
        if (!isAttack)
        {
            InvokeRepeating("FireType1", 0.5f, 1f);
            isAttack = true;
        }
    }

    void HandleAttackingType1State()
    {
        isRotate = true;
    }

    void fireType0()
    {
        foreach (BossWeapon weapon in weapons)
        {
            weapon.BossFireType0();
        }
    }

    void FireType1()
    {
        foreach (BossWeapon weapon in weapons)
        {
            weapon.BossFireType1();
        }
    }

    void InstaRegenerate()
    {
        health = maxHealth;
    }

    void DeactivateByDistance()
    {
        if (howFar > 50)
        {
            eCurState = BossActionType.Idle;
            CancelInvoke();
            isAttack = false;
        }
    }

    void Rotate()
    {
        if (isRotate)
        {
            transform.Rotate(Vector3.forward * 150 * Time.deltaTime);
        }
    }

    private void OnDisable()
    {
        DeactivateByDistance();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        health -= collision.GetComponent<IDamageAmount>().GetDamage();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

