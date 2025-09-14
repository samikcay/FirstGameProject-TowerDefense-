using UnityEngine;

public abstract class EntityClass : MonoBehaviour
{
    [Header("Defense & Basic Attributes")]
    public int level = 0;
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    public float healthRegen = 0f;
    public float armor = 50f;
    public float movementSpeed = 5f;

    [Header("Attack Attributes")]
    public float attackDamage = 20f;
    public float attackSpeed = 1f;
    public float attackRange = 2f;

    [Header("Value Attributes")]
    public float goldValue = 10f;

    private void Update()
    {
        level = GameControllerScript.RaidLevel;
    }

    public void GetDamage(float damage)
    {
        if (currentHealth > 0)
            currentHealth += damage;
    }
}