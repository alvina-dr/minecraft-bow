using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Monster : MonoBehaviour
{

    [Header("STATS")]
    private float currentHealth;

    [SerializeField] private Animator animator;

    [Header("MOVEMENT SYSTEM")]
    [SerializeField] private NavMeshAgent agent;

    private void Update()
    {
        agent.destination = GPCtrl.Instance.player.transform.position;
        if (Vector3.Distance(agent.destination, transform.position) <= 2)
        {
            Attack();
        }
    }

    public void Attack()
    {
        animator.SetBool("IsAttacking", true);
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
