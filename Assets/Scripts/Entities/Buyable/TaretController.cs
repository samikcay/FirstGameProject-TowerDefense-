using Unity.Mathematics;
using UnityEngine;

public class TaretController : MonoBehaviour
{
    private GameObject head;
    private Transform muzzleHead;

    public GameObject bulletPrefab;
    private float projectileSpeed = 20f;
    private float projectileLifeTime = 2f;
    private float fireInterval = 3f;
    private float lastFireTime = -Mathf.Infinity;

    private GameObject[] enemies;
    private GameObject nearestEnemy;

    enum State
    {
        Idle,
        Attack
    }

    private State currentState = State.Idle;

    private void Start()
    {
        head = transform.Find("Head").gameObject;
        muzzleHead = head.transform.Find("MuzzleHead");
        nearestEnemy = null;

        InvokeRepeating(nameof(CheckState), 0f, 0.5f);
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                // Boşta durma işlemleri burada yapılabilir
                break;
            case State.Attack:
                if (nearestEnemy == null)
                    FindEnemy();
                AttackToEnemy();
                // Saldırı işlemleri burada yapılabilir
                break;
        }
    }

    private void CheckState()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            State state = State.Attack;
            switch (state)
            {
                case State.Idle:
                    currentState = State.Idle;
                    break;
                case State.Attack:
                    currentState = State.Attack;
                    break;
            }
        }
    }

    private void FindEnemy()
    {
        if (enemies == null || enemies.Length == 0)
        {
            nearestEnemy = null;
            return;
        }

        float minDistance = Mathf.Infinity;
        nearestEnemy = null;

        foreach (var enemy in enemies)
        {
            if (enemy == null) continue; // destroyed olabilir

            // Görünebilirlik kontrolü: arada engel varsa pas geç
            if (!IsEnemyVisible(enemy)) continue;

            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }
    }

    private bool IsEnemyVisible(GameObject enemy)
    {
        if (enemy == null) return false;

        Vector3 rayDir = enemy.transform.position - muzzleHead.position;
        float rayDistance = rayDir.magnitude;
        if (rayDistance <= 0.0001f) return true;

        if (Physics.Raycast(muzzleHead.position, rayDir.normalized, out RaycastHit hit, rayDistance))
        {
            if (hit.collider == null) return true;
            // Eğer ray hedefe veya hedefin child'ına çarpıyorsa görünür say
            if (hit.collider.gameObject == enemy || hit.collider.transform.IsChildOf(enemy.transform))
                return true;

            // Başka bir obje engelliyor
            return false;
        }

        // Hiçbir şeye çarpmadıysa (ör. hedef collider yok) ateşe izin ver
        return true;
    }

    private void AttackToEnemy()
    {
        // Eğer geçerli hedef yoksa veya artık görünmüyorsa yeni uygun hedef ara
        if (nearestEnemy == null || !IsEnemyVisible(nearestEnemy))
        {
            FindEnemy();
        }

        if (nearestEnemy == null)
        {
            // Uygun hedef yok
            return;
        }

        Vector3 direction = nearestEnemy.transform.position - head.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(head.transform.rotation, lookRotation, Time.deltaTime * 5).eulerAngles;
        head.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // Hedef görünürse ateş et
        if (IsEnemyVisible(nearestEnemy))
        {
            ShootBullet();
        }
        else
        {
            // Eğer döndükten sonra da engel varsa hedefi temizle ki bir sonraki döngüde başka hedef seçilsin
            nearestEnemy = null;
        }
    }

    private void ShootBullet()
    {
        // Fire interval kontrollü: halen cooldown dolmadıysa çık
        if (Time.time - lastFireTime < fireInterval)
            return;

        lastFireTime = Time.time;

        GameObject proj = Instantiate(bulletPrefab, muzzleHead.position, muzzleHead.rotation);

        // Rigidbody ile ileri hız ver
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb == null) rb = proj.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearVelocity = muzzleHead.forward * projectileSpeed;

        Destroy(proj, projectileLifeTime);
    }
}