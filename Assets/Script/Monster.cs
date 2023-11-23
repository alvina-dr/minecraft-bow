using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Monster : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] Rigidbody rigibody;
    [SerializeField] List<Renderer> meshList = new List<Renderer>();

    [Header("STATS")]
    public MonsterData data;
    private float currentHealth;

    [SerializeField] private Animator animator;

    [Header("MOVEMENT SYSTEM")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float pushForce;

    private float attackTimer;

    private void Start()
    {
        currentHealth = data.maxHealth;
        agent.speed = data.moveSpeed;
    }

    private void Update()
    {
        if (agent.enabled) agent.destination = GPCtrl.Instance.player.transform.position;
        if (Vector3.Distance(agent.destination, transform.position) <= data.attackRange)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= data.attackReload) Attack();
        } else
        {
            animator.SetBool("IsAttacking", false);
        }
    }

    private void Attack()
    {
        animator.SetBool("IsAttacking", true);
        attackTimer = 0;
        GPCtrl.Instance.player.Damage(data.damage);
    }

    public void Damage(float _damage, Vector3 _pushDirection)
    {
        currentHealth -= _damage;
        Blink();
        audioSource.Play();
        rigibody.AddForce(new Vector3(_pushDirection.x, 0, _pushDirection.z).normalized * pushForce, ForceMode.Impulse);
        rigibody.AddForce(transform.up * 10, ForceMode.Impulse);
        transform.GetChild(0).DOLocalJump(Vector3.zero, .5f, 1, .5f);
        DOVirtual.DelayedCall(.2f, () =>
        {
            if (currentHealth <= 0)
            {
                Death();
            }
        });

    }

    public void Death()
    {
        GetComponent<Collider>().enabled = false;
        animator.SetTrigger("Death");
        DOVirtual.DelayedCall(1.5f, () =>
        {
            GPCtrl.Instance.UICtrl.deathCounter.Increment(data.killPoints);
            Destroy(this.gameObject);
        });
    }

    public void Blink()
    {
        for (int i = 0; i < meshList.Count; i++)
        {
            for (int j = 0; j < meshList[i].materials.Length; j++)
            {
                meshList[i].materials[j].color = Color.red;
            }
        }
        DOVirtual.DelayedCall(.2f, () => 
        {
            for (int i = 0; i < meshList.Count; i++)
            {
                for (int j = 0; j < meshList[i].materials.Length; j++)
                {
                    meshList[i].materials[j].color = Color.white;
                }
            }
        });

    }
}
