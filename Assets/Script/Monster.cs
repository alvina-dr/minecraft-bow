using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Monster : MonoBehaviour
{

    [Header("STATS")]
    public MonsterData data;
    private float currentHealth;

    [SerializeField] private Animator animator;

    [Header("MOVEMENT SYSTEM")]
    [SerializeField] private NavMeshAgent agent;

    private float attackTimer;

    private void Start()
    {
        currentHealth = data.maxHealth;
        agent.speed = data.moveSpeed;
    }

    private void Update()
    {
        agent.destination = GPCtrl.Instance.player.transform.position;
        if (Vector3.Distance(agent.destination, transform.position) <= data.attackRange)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= data.attackReload) Attack();
        }
    }

    private void Attack()
    {
        animator.SetBool("IsAttacking", true);
        attackTimer = 0;
        GPCtrl.Instance.player.Damage(data.damage);
    }

    public void Damage(float _damage)
    {
        currentHealth -= _damage;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        animator.SetTrigger("Death");
        DOVirtual.DelayedCall(1.5f, () =>
        {
            GPCtrl.Instance.UICtrl.deathCounter.Increment();
            Destroy(this.gameObject);
        });
    }
}
