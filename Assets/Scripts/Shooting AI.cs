using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingAI : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    [SerializeField] public float bulletspeed = 6f;
    [SerializeField] public float bulletDespawnTime = 2f;

    public void ShootStraight(Vector3 direct)
    {
        GameObject Bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(direct * bulletspeed, ForceMode2D.Impulse);
        Bullet.GetComponent<BulletDespawn>().gummytransform = transform;
        Destroy(Bullet, bulletDespawnTime);
    }

    public void ShootAngle(float angle){
        GameObject Bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();

        Quaternion quat = new();
        quat.eulerAngles = new Vector3 (0.0f, 0.0f, angle);

        firePoint.eulerAngles = new UnityEngine.Vector3(0.0f, 0.0f, angle);
        rb.AddForce((quat * Vector3.up) * bulletspeed, ForceMode2D.Impulse);

        Bullet.GetComponent<BulletDespawn>().gummytransform = transform;
        Destroy(Bullet, bulletDespawnTime);
    }

    public void ShootRandom()
    {
        GameObject Bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();

        firePoint.eulerAngles = new UnityEngine.Vector3(0.0f, 0.0f, UnityEngine.Random.Range(0.0f, 360.0f));
        rb.AddForce(firePoint.up * bulletspeed, ForceMode2D.Impulse);

        Bullet.GetComponent<BulletDespawn>().gummytransform = transform;
        Destroy(Bullet, bulletDespawnTime);
    }

    public void ShootPlayer()
    {
        Physics.IgnoreLayerCollision(7, 8);
        GameObject Bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();

        var playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        var shootDirection = playerPos - transform.position;
        shootDirection.Normalize();
        UnityEngine.Vector2 sd2 = new(shootDirection.x, shootDirection.y);

        Debug.Log(shootDirection);
        rb.AddForce(sd2 * bulletspeed, ForceMode2D.Impulse);
        Bullet.GetComponent<BulletDespawn>().gummytransform = transform;
        Destroy(Bullet, bulletDespawnTime);
    }
}
