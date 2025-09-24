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
        // Animator = attacking
        Debug.Log("Attacking the target!");
    }
}
