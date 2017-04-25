using UnityEngine;
using System.Collections;

public class DestroyCannonShell : MonoBehaviour 
{
	public GameObject cannonShellExplosion;
	EnemyHealth enemyHealth;
	//public AudioClip cannonShellDamageSound;
	
	void OnTriggerEnter(Collider other)	
	{
		if (other.tag == "Enemy" && !other.ToString ().Contains ("Sphere")) 
		{
			Debug.Log(other);
			Instantiate (cannonShellExplosion,transform.position,Quaternion.identity);
			//AudioSource.PlayClipAtPoint(cannonShellDamageSound,transform.position);
			Destroy(gameObject);
			enemyHealth = other.gameObject.GetComponentInChildren<EnemyHealth>();
			enemyHealth.TakeDamage(30);
		}
		else if (other.tag == "Ground")
		{
			Instantiate (cannonShellExplosion,transform.position,Quaternion.identity);
			//AudioSource.PlayClipAtPoint(cannonShellDamageSound,transform.position);
			Destroy(gameObject);
		}
	}  
}
