using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthGUIScript : MonoBehaviour {
	
	//GUI
	public Slider batterySilder;
	private Color fullBattery =  new Color(0f,1f,0f,1f);
	private Color highMidBattery =  new Color(1f,1f,0f,1f);
	private Color midBattery =  new Color(1f,.5f,0f,1f);
	private Color lowBattery =  new Color(1f,0f,0f,1f);

	//public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColor =  new Color(1f,0f,0f,0.1f);
	public AudioClip deathSound;
	//end GUI

	public float silderValue = 100;

	public float SilderValue
	{
		get { return silderValue; }
		set { silderValue = value; }
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		batterySilder.value = silderValue;
		
		if (silderValue >= 75 )
		{
			batterySilder.GetComponentInChildren<Image>().color = fullBattery;
		}
		else if (silderValue < 75 && silderValue >= 50)
		{
			batterySilder.GetComponentInChildren<Image>().color = highMidBattery;
		}
		else if (silderValue < 50 && silderValue >= 25)
		{
			batterySilder.GetComponentInChildren<Image>().color = midBattery;
		}
		else if (silderValue < 25)
		{
			batterySilder.GetComponentInChildren<Image>().color = lowBattery;
		}
	}


}
