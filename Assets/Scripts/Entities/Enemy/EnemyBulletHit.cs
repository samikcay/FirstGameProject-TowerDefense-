using UnityEngine;

public class EnemyBulletHit : MonoBehaviour
{
    public GameObject coin;

    private void OnCollisionEnter(Collision collision)
    {
        // Düþman öldüðünde
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Instantiate(coin, transform.position, coin.transform.rotation);
        }
    }
}
