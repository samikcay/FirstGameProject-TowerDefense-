using UnityEngine;
using UnityEngine.Rendering;

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
    private Collider towerCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("MainTower");
            towerCollider = target.GetComponent<Collider>();
        }
    }

    private void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, towerCollider.ClosestPointOnBounds(transform.position));
        Debug.Log(distanceToTarget);
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
        animator.SetFloat("Speed", 1);
        animator.SetBool("IsAttacking", false);
        // Hedefin sadece XZ konumunu al, düþmanýn Y konumunu koru
        Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);

        // Hedefe doðru güvenli bir þekilde ilerle (taþma/overshoot önlenir)
        transform.position = Vector3.MoveTowards(transform.position, targetPos, movementSpeed * Time.deltaTime);

        Vector3 lookDir = targetPos - transform.position;
        if (lookDir.sqrMagnitude > 0.0001f)
        {
            transform.rotation = Quaternion.LookRotation(lookDir);
        }

        // Animator = walking
    }

    private void AttackTarget()
    {
        // Implement attack logic here
        animator.SetFloat("Speed", 0);
        animator.SetBool("IsAttacking", true);
        Debug.Log("Attacking the target!");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
