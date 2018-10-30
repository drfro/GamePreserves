using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunGunScript : BaseGunScript
{
    public int numPellets;

    public override void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < numPellets; i++)
            {
                Vector2 gunPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = new Vector2(worldPoint.x + Random.Range(-1.0f, 1.0f), worldPoint.y + Random.Range(-1.0f, 1.0f)) - gunPos;
                direction.Normalize();
                // Spawn bullet object from player position moving in vector between player and mouse cursor.

                GameObject shotFired = Instantiate(bulletPrefab, new Vector3(gunPos.x, gunPos.y, 0), Quaternion.identity);
                shotFired.GetComponent<Rigidbody2D>().velocity = direction * (bulletSpeed + Random.Range(-1.0f, 1.0f));
                shotFired.GetComponent<BulletScript>().bulletDamage = bulletDamage;
            }
        }
        
    }
}
