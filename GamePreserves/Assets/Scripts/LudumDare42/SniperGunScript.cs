using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class SniperGunScript : BaseGunScript
{
    public GameObject laserBeam;

    public override void Start()
    {
//        laserBeam = GameObject.FindGameObjectWithTag("Bullet");
//        laserBeam.active = false;
        
        base.Start();
    }

    public override void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 gunPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = new Vector2(worldPoint.x, worldPoint.y) - gunPos;
            direction.Normalize();
            Vector2 endPos = (direction * 10) + gunPos;

            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(1 << LayerMask.NameToLayer("Character"));
            RaycastHit2D[] results = new RaycastHit2D[10];

            Physics2D.Linecast(gunPos, endPos, filter, results);

            foreach(RaycastHit2D hit in results)
            {
                if(hit.collider && hit.collider.tag == "Enemy")
                {
                    hit.transform.gameObject.GetComponent<EnemyTopDownMovement>().TakeDamage(bulletDamage);
                    
                }

            }
            fireZeeLaser(mouseLocation());
           // StartCoroutine(Fade());

        }
        else
        {
            //laserBeam.active = false;
           
        }
    }
    
//    IEnumerator Fade() {
//        yield return new WaitForSeconds(.5f);
//        laserBeam.enabled = false;
//    }

    void fireZeeLaser(float attackAngle)
    {
       laserBeam.GetComponent<LaserBlastScript>().laserFired(attackAngle);
    }

    void CleanUpLaser()
    {
        laserBeam.active = false;
    }

    private float mouseLocation()
    {
        var mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var dirVector = mouseWorldPoint - this.transform.position;
        dirVector.Normalize();
        
        var angle = Mathf.Atan2(dirVector.y, dirVector.x);

        return Mathf.Rad2Deg * angle + 90;
    }
    
}
