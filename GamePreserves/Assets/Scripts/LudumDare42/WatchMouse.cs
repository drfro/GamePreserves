using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchMouse : MonoBehaviour
{
    private Animator anim;

	// Use this for initialization
	void Start ()
	{
	    anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

	    var dirVector = mouseWorldPoint - this.transform.position;
        dirVector.Normalize();

	    var angle = Mathf.Atan2(dirVector.y, dirVector.x);
	    var ratio = angle / (Mathf.PI * 2);

	    if (ratio < 0)
	    {
	        ratio = ratio + 1;
	    }

	    anim.SetFloat("rotation", ratio);

//        Debug.Log(ratio);
	}
}
