using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlastScript : MonoBehaviour
{

	public Animator laserAnimator;

	public SpriteRenderer laserImage;
	// Use this for initialization
	void Start ()
	{
		laserImage.enabled = false;
	}
	
	
	public void laserFired(float direction)
	{
		laserImage.enabled = true;
		
		this.transform.eulerAngles = new Vector3(0,0,direction);
		
		laserAnimator.SetTrigger("blastLaser");
	}

	void turnoffLaser()
	{
		laserImage.enabled = false;
	}
}
