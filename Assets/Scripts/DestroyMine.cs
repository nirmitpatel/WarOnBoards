using UnityEngine;
using System.Collections;

public class DestroyMine : MonoBehaviour 
{
	public GameObject mineExplosion;
	EnemyHealth enemyHealth = null;
	PlayerHealth playerHealth;
	bool isTriggered = false;
	public AudioClip mineTrigger;

	void Start()
	{
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") // && other.tag == "Wheel" && !isTriggered) 
		{
			isTriggered = true;
			StartCoroutine (MineDestruction (other.transform.root.gameObject.tag));
		} 
		else if (other.tag == "Enemy") // && other.tag == "Wheel") 
		{
			StartCoroutine (MineDestruction (other.transform.root.gameObject.tag));
			enemyHealth = other.transform.root.gameObject.GetComponentInChildren<EnemyHealth> ();
		} 
		else if(other.tag == "Cannon" || other.tag == "LGBomb")
			StartCoroutine (MineDestruction (other.tag));

	}

	IEnumerator MineDestruction(string triggeredObject)
	{
		AudioSource.PlayClipAtPoint (mineTrigger,transform.position);
		yield return new WaitForSeconds(0.5f);
		Instantiate (mineExplosion, transform.position, Quaternion.identity);
		Destroy (gameObject.transform.parent.gameObject);
		if (triggeredObject == "Player") 
		{
			playerHealth.TakeDamage (50);
		} 
		else if (triggeredObject == "Enemy") 
		{
			enemyHealth.TakeDamage (50);
		}
		//Destroy (gameObject);
	}

}
