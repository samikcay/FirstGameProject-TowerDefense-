using UnityEngine;

public class EnemyController : EntityClass
{
    enum EnemyState
    {
        Chasing,
        Attacking
    }

    private GameObject target;
    private float distanceToTarget;
    private EnemyState currentState;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("MainTower");
        }
    }

    private void Start()
    {
        level = 1;
        maxHealth = 100f;
        currentHealth = maxHealth;
        healthRegen = 0f;
        armor = 50f;
        movementSpeed = 3f;
        attackDamage = 20f;
        attackSpeed = 1f;
        attackRange = 2f;
        goldValue = 10f;
    }

    private void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        currentState = ChechEnemyState();

        switch (currentState)
        {
            case EnemyState.Chasing:
                ChaseTarget();
                break;
            case EnemyState.Attacking:
                AttackTarget();
                break;
        }
    }

    private EnemyState ChechEnemyState()
    {
        if (distanceToTarget <= attackRange)
            return EnemyState.Attacking;

        return EnemyState.Chasing;
    }

    private void ChaseTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.position += movementSpeed * Time.deltaTime * direction;
        // Animator = walking
    }

    private void AttackTarget()
    {
        // Implement attack logic here
        // Animator = attacking
        Debug.Log("Attacking the target!");
    }
}
