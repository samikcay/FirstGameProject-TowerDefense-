using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public int roundIntervalTime = 30; // seconds
    public Vector3 spawnpoint = new Vector3(500, 0, 500);

    private static int raidLevel = 0;
    public static int RaidLevel
    {
        get { return raidLevel; }
    }
    private MainTower tower;
    [SerializeField]
    public GameObject enemies;

    private void Start()
    {
        tower = GameObject.FindGameObjectWithTag("MainTower").GetComponent<MainTower>();
        tower.currentHealth = tower.maxHealth;
        StartCoroutine(WaveSpawner());
    }

    private void Update()
    {
        CheckTowerHealth();
    }

    IEnumerator WaveSpawner()
    {
        yield return new WaitForSeconds(5f);

        while (tower.currentHealth > 0)
        {
            raidLevel++;
            Debug.Log("Raid Level: " + raidLevel);

            // Spawn enemies based on raidLevel here
            for (int i = 0; i < raidLevel * 2; i++)
            { 
                // Example: spawn more enemies as raidLevel increases
                Instantiate(enemies, new Vector3(Random.Range(-20, 20), 1, Random.Range(-10, 10)) + spawnpoint, Quaternion.identity);
                Debug.Log("Spawned Enemy");
                yield return new WaitForSeconds(1f); // Slight delay between spawns
            }

            yield return new WaitForSeconds(roundIntervalTime); // Wait for 30 seconds before next wave
        }
    }

    private void CheckTowerHealth()
    {
        if (tower.currentHealth <= 0)
        {
            //GameOver();
        }
    }

    //private void GameOver()
    //{
    //    Debug.Log("Game Over! The Main Tower has been destroyed.");
    //    // Implement additional game over logic here (e.g., stop spawning, show UI, etc.)
    //    StopAllCoroutines();
    //    enemies = GameObject.FindGameObjectsWithTag("Enemy");
    //    foreach (var enemy in enemies)
    //    {
    //        Destroy(enemy);
    //    }
    //    // Oyun sonu sahnesi
    //}
}
