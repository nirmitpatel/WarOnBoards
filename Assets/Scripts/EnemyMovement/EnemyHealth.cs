using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour 
{
	public int startingHealth = 5;            
	public int currentHealth; 
	public GameObject enemyExplosion;
	public Image healthtrasform;
	private HUD hud;
	private DoneLastPlayerSighting dlps;
	void Awake ()
	{
		dlps = GameObject.FindGameObjectWithTag ("GameController").GetComponent<DoneLastPlayerSighting> ();
		currentHealth = startingHealth;
		hud = GameObject.FindGameObjectWithTag ("UI").GetComponent<HUD> ();
		healthtrasform.fillAmount = currentHealth / startingHealth;
	}
	
	public void TakeDamage (int amount)
	{
		currentHealth -= amount;
		healthtrasform.fillAmount =(float) currentHealth / startingHealth;
		Debug.Log ((float)currentHealth / startingHealth);
		if(currentHealth <= 0)
		{
			Death ();
		}
	}

	void Death ()
	{
		Destroy(gameObject.transform.parent.gameObject);
		Instantiate (enemyExplosion, transform.position, Quaternion.identity);
		hud.UpdateScore (10);
		dlps.death_count++;
	}
}
