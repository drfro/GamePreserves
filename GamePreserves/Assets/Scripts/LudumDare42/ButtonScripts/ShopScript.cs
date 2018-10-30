using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopScript : MonoBehaviour {

    //Guns
    public GameObject defaultGun;
    public GameObject shotgunGun;
    public GameObject sniperGun;
    public GameObject globeGun;
    public GameObject currentGun;

    public int bonusHealth;

    public GameObject Roomba;

    //Buttons in inspector
    public Button defaultGunButton;
    public Button shotgunButton;
    public Button sniperGunButton;
    public Button globeGunButton;
    public DialogueEngine engineObject;

    private AudioSource audioPlayer;
    public AudioClip buySound;

    // Use this for initialization
    void Start () {

        //Capture objects
        Button btn1 = defaultGunButton.GetComponent<Button>();
        Button btn2 = shotgunButton.GetComponent<Button>();
        Button btn3 = sniperGunButton.GetComponent<Button>();
        Button btn4 = globeGunButton.GetComponent<Button>();

        //Add Listeners
        btn1.onClick.AddListener(SetGunToDefault);
        btn2.onClick.AddListener(SetGunToShotgun);
        btn3.onClick.AddListener(SetGunToSniper);
        btn4.onClick.AddListener(SetGunToGlobe);

        audioPlayer = GetComponent<AudioSource>();

        defaultGun = FindGunInInventory("DefaultGun");
        shotgunGun = FindGunInInventory("ShotgunGun");
        sniperGun = FindGunInInventory("SniperGun");
        globeGun = FindGunInInventory("GlobeGun");
        

//        Roomba = GameObject.Find("Roomba");
//        Roomba.SetActive(false);

//        engineObject = GameObject.FindObjectOfType<DialogueEngine>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetGunToDefault()
    {
        currentGun = defaultGun;
        engineObject.chosenChoiceAction(0);
        playBuySound();
        SetCurrentGun(currentGun);
    }

    public void SetGunToShotgun()
    {
        currentGun = shotgunGun;
        engineObject.chosenChoiceAction(2);
        playBuySound();
        SetCurrentGun(currentGun);
    }

    public void SetGunToSniper()
    {
        currentGun = sniperGun;
        engineObject.chosenChoiceAction(3);
        playBuySound();
        SetCurrentGun(currentGun);

    }

    public void SetGunToGlobe()
    {
        currentGun = globeGun;
        engineObject.chosenChoiceAction(4);
        playBuySound();
        SetCurrentGun(currentGun);
    }

    public void SetCurrentGun(GameObject gunToUse)
    {
        GameObject.Find("GameStatus").GetComponent<GameStatusScript>().currentGun = gunToUse;
    }

    public GameObject FindGunInInventory(String gun)
    {
        GameObject inventoryGun = GameObject.Find("GameStatus").GetComponent<GameStatusScript>().gunsInInventory
            .Find(obj => obj.name == gun);
        return inventoryGun;
    }

    public void openShop()
    {

    }

    private void playBuySound()
    {
        if (audioPlayer != null && buySound != null)
        {
            audioPlayer.PlayOneShot(buySound, .8f);
        }
    }


    public void closeShop()
    {

        SceneManager.LoadScene("MainGame");
    }

}
