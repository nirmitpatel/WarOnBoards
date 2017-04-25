using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	EnemyHealth enemyHealth;
	//public GameObject enemy;

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Enemy" && !other.ToString ().Contains ("Sphere")) 
		{
			ExplodeBomb();
			enemyHealth = other.gameObject.GetComponentInChildren<EnemyHealth> ();
			enemyHealth.TakeDamage (100);
		} 
		else if (other.tag == "Player") 
		{
			ExplodeBomb();
		}
		else if(other.tag=="Ground" || other.tag == "Missile")
		{
			ExplodeBomb();
		}
	}

	void ExplodeBomb()
	{
		Instantiate (explosion, transform.position + new Vector3 (0f, 1f, 0f), Quaternion.identity);
		Destroy (gameObject);
	}
}