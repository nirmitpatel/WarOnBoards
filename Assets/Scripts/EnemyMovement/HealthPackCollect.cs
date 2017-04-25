using UnityEngine;
using System.Collections;

public class HealthPackCollect : MonoBehaviour {
	// Use this for initialization
	private GameObject player;
	public GameObject HealthPack;
	private PlayerHealth playerHealth;
	private EnemyHealth enemyHealth;
	private GameObject enemy;
	HUD hud;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		hud = GameObject.FindGameObjectWithTag ("UI").GetComponent<HUD> ();
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			HealthPack.SetActive(false);

			if(playerHealth.currentHealth + 50 > 100)
			{
				playerHealth.currentHealth = 100;
			}
			else
			{
				playerHealth.currentHealth +=50;
			}
			hud.ChangeHealth(playerHealth.currentHealth);
		}
		else if (other.gameObject.CompareTag("Enemy")&& isNotEnemySphereCollider(other))
		{
			HealthPack.SetActive(false);
			enemyHealth = other.gameObject.GetComponentInChildren<EnemyHealth>();
			if(enemyHealth.currentHealth + 50 > 100)
			{
				enemyHealth.currentHealth = 100;
			}
			else
			{
				enemyHealth.currentHealth +=50;
			}
		}
	}

	bool isNotEnemySphereCollider(Collider other)
	{
		if (other.tag == "Enemy" && other.ToString().Contains("Sphere")) 
		{
			return false;
		}
		return true;
	}



}
