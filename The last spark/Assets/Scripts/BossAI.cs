using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {

    public Transform player;
    Vector3 firstPos;
    bool isAttack;
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
        weapons = new List<BossWeapon> { weapon0, weapon1, weapon2, weapon3, weapon4, weapon5 };
    }
	
	// Update is called once per frame
	void Update () {
        howFar = Vector3.Distance(transform.position, player.position);

        if (howFar > 50)
        {
            eCurState = BossActionType.Idle;
            CancelInvoke();
            isAttack = false;
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
        }
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
            InvokeRepeating("Rotate", 0.5f, 1f);
            isAttack = true;
        }
    }

    void FireType1()
    {
        foreach(BossWeapon weapon in weapons)
        {
            weapon.BossFireType1();
        }
    }

    void fireType0()
    {
        foreach (BossWeapon weapon in weapons)
        {
            weapon.BossFireType0();
        }
    }

    void Rotate()
    {

    }

    void InstaRegenerate()
    {
        health = maxHealth;
    }

}
