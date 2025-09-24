using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private PlayerPropertiesScript scrpt = null;

    private void Update()
    {
        gameObject.transform.Rotate(new Vector3(90, 0, 0) * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            scrpt = collision.gameObject.GetComponent<PlayerPropertiesScript>();
            scrpt.Gold += 10;
            Destroy(gameObject);
        }
    }
}
