using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] float UltCooldown = 10.0f;
    [SerializeField] float AttackRate = 2.0f;
    [SerializeField] float ShotgunRate = 6.0f;
    float CurrentCooldown;    // Cooldown of Ult
    float CurrentAttackCD;   // CD = Cooldown of Ultimate
    bool AttackActiveCD = true;

    float CurrentShotgunCD;   // CD = Cooldown of Ultimate
    bool ShotgunActiveCD = true;
    bool CooldownActive = true;
    ShootingAI Attack;
    [SerializeField] float AngleBullet = 1;

    void Start()
    {
        CurrentCooldown = UltCooldown;
        Attack = GetComponent<ShootingAI>();
    }

    void Update()
    {
        if (!UltTimer())
        {
            Ultimate();
            CurrentCooldown = UltCooldown;
            CooldownActive = true;
        }
        if (!AttackTimer())
        {
            BossAttack();
            CurrentAttackCD = AttackRate;
            AttackActiveCD = true;
        }

        if (!ShotgunTimer())
        {
            ShotgunAttack();
            CurrentShotgunCD = ShotgunRate;
            ShotgunActiveCD = true;
        }

    }
    void Ultimate()
    {
        for (float i = 0; i < 360;)
        {
            Attack.ShootAngle(i);
            i += AngleBullet;
        }
    }

    void BossAttack()
    {
        Attack.ShootPlayer();
        Attack.ShootRandom();
    }

    void ShotgunAttack()
    {
        for (int i = 0; i < 16; i++)
        {
            Vector3 offset = new(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), 0.0f);
            var playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

            Vector3 target = playerPos - transform.position;
            target.Normalize();
            target += offset;
            Attack.ShootStraight(target);

        }
    }

    bool UltTimer()
    {
        if (CooldownActive && CurrentCooldown >= 0.0f)
        {
            CurrentCooldown -= Time.deltaTime;
        }
        if (CurrentCooldown <= 0.0f)
        {
            CooldownActive = false;
        }
        return CooldownActive;
    }

    bool AttackTimer()
    {
        if (AttackActiveCD && CurrentAttackCD >= 0.0f)
        {
            CurrentAttackCD -= Time.deltaTime;
        }
        if (CurrentAttackCD <= 0.0f)
        {
            AttackActiveCD = false;
        }
        return AttackActiveCD;
    }

    bool ShotgunTimer()
    {
        if (ShotgunActiveCD && CurrentShotgunCD >= 0.0f)
        {
            CurrentShotgunCD -= Time.deltaTime;
        }
        if (CurrentShotgunCD <= 0.0f)
        {
            ShotgunActiveCD = false;
        }
        return ShotgunActiveCD;
    }
}
