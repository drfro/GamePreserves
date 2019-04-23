using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{

	private float hp = 100;
	public bool isDead = false;

	public bool IsDead
	{
		get { return isDead; }
		set { isDead = value; }
	}

	public bool isEquiped = true;

	public bool IsEquiped
	{
		get { return isEquiped; }
		set { isEquiped = value; }
	}

	public Vector2 playerStartPos;

    //Goo Varaibles
    private float startTime;
	private float curTime;
	public float dmgTick = 2.0f;
	private bool inGoo = false;
	public int gooDmg = 0;

	//end Goo

	public bool InGoo
	{
		get { return inGoo; }
		set { inGoo = value; }
	}


//	private void Awake()
//	{
//		DontDestroyOnLoad(gameObject);
//	}

	// Use this for initialization
	void Start ()
	{
		hp = GameObject.Find("GameStatus").GetComponent<GameStatusScript>().playerMaxHealth;
        playerStartPos = gameObject.transform.position;
       
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!isDead)
		{
			this.GetComponent<PlayerTopDownMovement>().animator.SetBool("dead", false);
			//Player Standing in Goo
			if (inGoo)
			{
				curTime = Time.time;

				if (curTime - startTime > dmgTick)
				{
					TakeDamage(gooDmg);
					ResetTimer();
				}
			}
		}
		else
		{
			
		}

	}

	public void TakeDamage(float dmg)
	{
		hp -= dmg;

		GameObject.Find("EventSystem").GetComponent<HealthGUIScript>().SilderValue = hp;
		
		if(hp <= 0)
		{
			Death();
		}
	}
	
	public void Death()
	{
		isDead = true;
		EventManager.TriggerEvent("PLAYER_DIED");
		this.GetComponent<PlayerTopDownMovement>().animator.SetBool("dead", true);
		this.GetComponent<PlayerTopDownMovement>().animator.SetFloat("speed", 0.0f);
	}

	public void Cleanup()
	{
		SceneManager.LoadScene("ShopScene");
		Cursor.SetCursor(null, new Vector2(0.0f, 0.0f), CursorMode.Auto);
	}
	
	private void ResetTimer()
	{
		startTime = Time.time;
		curTime = 0.0f;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "GooPile")
		{
			gooDmg = other.GetComponent<GooScript>().dmg;
			inGoo = true;
			ResetTimer();
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "GooPile")
		{
			inGoo = false;
			gooDmg = 0;
			ResetTimer();
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "GooPile")
		{
			gooDmg = other.GetComponent<GooScript>().dmg;
			inGoo = true;

		}
	}
    
    public void GrantBonusHealth(int bonusHealth)
    {
        hp += bonusHealth;
    }
	
    //Run when leaving the shop and returning to the game
    public void ResetPlayer()
    {
        hp = GameObject.Find("GameStatus").GetComponent<GameStatusScript>().playerMaxHealth;
        isDead = false;
        gameObject.transform.position = playerStartPos;


    }
	
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody && collision.rigidbody.tag == "Enemy" && !IsDead)
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyTopDownMovement>().bumpDamage);
        }
    }
    
}
