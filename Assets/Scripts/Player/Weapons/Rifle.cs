using UnityEngine;

public class Rifle : Weapons
{
    private float cooldown = 0f;
    public float intervalBetweenAttacks = 1.5f;
    

    private void Update()
    {
        while (cooldown < intervalBetweenAttacks)
        {
            cooldown += Time.deltaTime;
        }

        //Shoot();
    }

    private void Shoot(Vector3 direction)
    {

    }
}
