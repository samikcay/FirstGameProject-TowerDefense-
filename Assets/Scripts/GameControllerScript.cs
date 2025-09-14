using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    private static int raidLevel = 0;
    public static int RaidLevel
    {
        get { return raidLevel; }
    }
    private MainTower tower;
    [SerializeField]
    private GameObject[] enemies;

    private void Start()
    {
        tower = GameObject.FindGameObjectWithTag("MainTower").GetComponent<MainTower>();
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

            yield return new WaitForSeconds(30f); // Wait for 30 seconds before next wave
        }
    }

    private void CheckTowerHealth()
    {
        if (tower.currentHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over! The Main Tower has been destroyed.");
        // Implement additional game over logic here (e.g., stop spawning, show UI, etc.)
        StopAllCoroutines();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
        // Oyun sonu sahnesi
    }
}
