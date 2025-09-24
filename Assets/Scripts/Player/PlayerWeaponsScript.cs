using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsScript : MonoBehaviour
{
    [Header("Ateş Ayarları")]
    public Transform muzzle;               // Merminin çıkacağı nokta
    public GameObject projectilePrefab;    // Mermi prefabı
    public float projectileSpeed = 20f;    // Mermi hızı
    public float projectileLifeTime = 2f;  // Merminin yaşam süresi
    public float fireInterval = 1f;        // Kaç saniyede bir ateş etsin

    private float timer;

    void Start()
    {
        if (muzzle == null) muzzle = transform; // Muzzle boşsa kendi transform'unu kullan
        timer = fireInterval; // Başta hemen ateş etsin istiyorsan =0 yap
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Fire();
            timer = fireInterval; // Sayacı sıfırla
        }
    }

    void Fire()
    {
        GameObject proj = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);

        // Rigidbody ile ileri hız ver
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb == null) rb = proj.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearVelocity = muzzle.forward * projectileSpeed;

        Destroy(proj, projectileLifeTime);
    }
}
