using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Transform player;
    public float health = 500;


    SpriteRenderer spriteRend;
    Color firstColor;
    //Vector3 firstPos; Gdy boss będzie się mógł poruszać

    bool isAttack = false;
    bool isImmortal = false;
    float degRotate = 0;
    float maxHealth;
    float howFar;
    float curDamage = 0;
    float wholeDamage = 0;

    float firstTimeMeas;

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
        FinalAction,
        AvoidingObstacle,
        AttackingType0,
        AttackingType1,
        AttackingType2
    }

    private BossActionType eCurState = BossActionType.Idle;


    // Use this for initialization
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        firstColor = spriteRend.color;
        maxHealth = health;
        weapons = new List<BossWeapon> { weapon0, weapon1, weapon2, weapon3, weapon4, weapon5 };
    }

    // Update is called once per frame
    void Update()
    {
        howFar = Vector3.Distance(transform.position, player.position);

        DeactivateByDistance();
        Rotate();
        ColorBoss();

        if (health <= 0)
        {
            Die();
        }

        switch (eCurState)
        {
            case BossActionType.Idle:
                HandleIdleState();
                break;

            case BossActionType.FinalAction:
                HandleFinalActionState();
                break;

            case BossActionType.AvoidingObstacle:
                HandleAvoidingObstacleState();
                break;

            case BossActionType.AttackingType0:
                HandleAttackingType0State();
                break;

            case BossActionType.AttackingType1:
                HandleAttackingType1State();
                break;

            case BossActionType.AttackingType2:
                HandleAttackingType2State();
                break;
        }
    }
    void HandleAvoidingObstacleState()
    {
        if (!isAttack)
        {
            firstTimeMeas = Time.time;
            degRotate = 360;
            isImmortal = true;
            isAttack = true;
        }
        if (Time.time - firstTimeMeas > 2)
        {
            eCurState = BossActionType.AttackingType1;
            isAttack = false;
            isImmortal = false;
            degRotate = 0;
        }
    }
    void HandleIdleState()
    {
        InstaRegenerate();

        if (howFar < 50)
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

        if (health < (0.6 * maxHealth))
        {
            CancelInvoke();
            eCurState = BossActionType.AttackingType1;
            isAttack = false;
        }
    }

    void HandleAttackingType1State()
    {
        if (!isAttack)
        {
            firstTimeMeas = Time.time;
            InvokeRepeating("FireType0", 0.5f, 0.1f);
            isAttack = true;
            degRotate = 215;
        }
        if (Time.time - firstTimeMeas > 5)
        {
            CancelInvoke();
            eCurState = BossActionType.AttackingType2;
            isAttack = false;
            degRotate = 0;
        }
        if (health < (0.1 * maxHealth))
        {
            CancelInvoke();
            eCurState = BossActionType.FinalAction;
            isAttack = false;
        }
    }

    void HandleAttackingType2State()
    {
        if (!isAttack)
        {
            firstTimeMeas = Time.time;
            InvokeRepeating("FireType1", 0.5f, 0.4f);
            isAttack = true;
        }
        if (health < (0.1 * maxHealth))
        {
            CancelInvoke();
            eCurState = BossActionType.FinalAction;
            isAttack = false;
        }
        if (Time.time - firstTimeMeas > 5)
        {
            CancelInvoke();
            eCurState = BossActionType.AvoidingObstacle;
            isAttack = false;
        }
         
    }

    void HandleFinalActionState()
    {
        if (!isAttack)
        {
            InvokeRepeating("FireType1", 0.5f, 0.1f);
            isAttack = true;
            degRotate = 215;
        }
    }

    void FireType0()
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

    void FireType2()
    {
        foreach (BossWeapon weapon in weapons)
        {
            weapon.BossFireType2();
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
        transform.Rotate(Vector3.forward * degRotate * Time.deltaTime);
    }

    private void OnDisable()
    {
        DeactivateByDistance();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isImmortal && !collision.CompareTag("Player") && !collision.gameObject.CompareTag("Osm"))
        {
            float damage = collision.GetComponent<IDamageAmount>().GetDamage();
            wholeDamage += damage;
            curDamage = wholeDamage;
            health -= damage;
        }
    }

    void ColorBoss()
    {
        if (wholeDamage > 0)
        {
            //Zmiana koloru przy otrzymaniu obrażeń
            spriteRend.color = Color.Lerp(spriteRend.color, Color.green, Mathf.PingPong(Time.time, 0.1f));
            wholeDamage -= curDamage * Time.deltaTime * 5;
        }
        else
        {
            spriteRend.color = Color.Lerp(spriteRend.color, firstColor, Mathf.PingPong(Time.time, 0.2f));
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
        CancelInvoke();
    }
}
