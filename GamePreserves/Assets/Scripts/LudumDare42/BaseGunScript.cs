using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BaseGunScript : MonoBehaviour {

    private GameObject player;
    public GameObject bulletPrefab;
    public int bulletDamage = 0;
    public float bulletSpeed;
    public float cooldownPeriod = 1f;
    private float cooldownTimerStart = 0;

    private AudioSource audioPlayer;
    public AudioClip fireSound;

	public Sprite crosshair;

	// Use this for initialization
	public virtual void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
		EventManager.StartListening("PLAYER_DIED", Cleanup);

		SetCursor();

	    audioPlayer = this.GetComponent<AudioSource>();
	}

    public void TryShoot()
    {

	    if (player && !player.GetComponent<PlayerStatus>().IsDead)
	    {
		    if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
		    {
			    if ((cooldownTimerStart == 0) || (Time.time > cooldownTimerStart + cooldownPeriod))
			    {
				    Shoot();
				    cooldownTimerStart = Time.time;
			        if (fireSound != null)
			        {
//			            Debug.Log("Pew!");
			            audioPlayer.PlayOneShot(fireSound, .75f);
			        }
                }
		    }
	    }
    }

    public virtual void Shoot()
    {
        Vector2 gunPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(worldPoint.x, worldPoint.y) - gunPos;
        direction.Normalize();
        // Spawn bullet object from player position moving in vector between player and mouse cursor.
        GameObject shotFired = Instantiate(bulletPrefab, new Vector3(gunPos.x, gunPos.y, 0), Quaternion.identity);
        shotFired.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        shotFired.GetComponent<BulletScript>().bulletDamage = bulletDamage;

    }

	public void Cleanup()
	{
		Destroy(this.gameObject);
	}


	public void SetCursor()
	{
		if (crosshair != null)
		{
			var croppedTexture = new Texture2D((int) crosshair.rect.width, (int) crosshair.rect.height);
			var pixels = crosshair.texture.GetPixels((int) crosshair.textureRect.x,
				(int) crosshair.textureRect.y,
				(int) crosshair.textureRect.width,
				(int) crosshair.textureRect.height);
			croppedTexture.SetPixels(pixels);
			croppedTexture.Apply();

			Cursor.SetCursor(croppedTexture, new Vector2(0.0f, 0.0f), CursorMode.Auto);
		}
	}

}
