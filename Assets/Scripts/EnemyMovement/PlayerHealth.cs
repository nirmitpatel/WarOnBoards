using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;            
	public int currentHealth; 
	public GameObject playerTankExplosion;
	HUD hud;
	public AudioClip playerTankExplosionSound;

	void Awake ()
	{
		currentHealth = startingHealth;
		hud = GameObject.FindGameObjectWithTag ("UI").GetComponent<HUD> (); 
	}

//	void Start()
//	{
//		hud.ChangeHealth (currentHealth);
//	}
	
	public void TakeDamage (int amount)
	{
		currentHealth -= amount;
		hud.ChangeHealth (currentHealth);
		if(currentHealth <= 0)
		{
			Death ();
		}
	}
	
	void Death ()
	{
		Destroy(gameObject);
		AudioSource.PlayClipAtPoint (playerTankExplosionSound,transform.position);
		Instantiate (playerTankExplosion, transform.position, Quaternion.identity);
	}
}
