using UnityEngine;

public class MainTower : EntityClass
{
    private void Start()
    {
        maxHealth = 1000f;
        currentHealth = maxHealth;
        healthRegen = 0f;
        armor = 100f;
        movementSpeed = 0f;
        attackDamage = 0f;
        attackSpeed = 0f;
        attackRange = 5f;
        goldValue = 0f;
    }
}
