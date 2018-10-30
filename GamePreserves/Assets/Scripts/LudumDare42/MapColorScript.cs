using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapColorScript : MonoBehaviour {

	
	Color[] colors =
	{
		new Color(0,1,0,1), 
		new Color(1,0,0,1), 
		new Color(1,1,1,1), 
		new Color(0,0,1,1),  
		new Color(1,1,0,1), 
		new Color(0,1,1,0),
		new Color(1,0,1,1)
	};

	private SpriteRenderer spriteRenderer;
	
	// Use this for initialization
	void Start ()
	{
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.color = colors[Random.Range(0,colors.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
