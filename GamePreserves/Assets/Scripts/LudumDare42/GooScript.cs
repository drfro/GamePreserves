using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooScript : MonoBehaviour
{
	

	public int dmg = 10;

    public float dmgOverTime = .4f;
	

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            //this thing is likely a ray, give ourselves to it
            other.gameObject.GetComponent<CleanerRay>().doThingToGoo(this.gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            //this thing is likely a ray, give ourselves to it
            other.gameObject.GetComponent<PlayerStatus>().TakeDamage(dmg);
        }

    }

    public void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            //this thing is likely a ray, give ourselves to it
            other.gameObject.GetComponent<PlayerStatus>().TakeDamage(dmgOverTime * Time.deltaTime);
        }

    }


}
