using UnityEngine;
using System.Collections;

public class E_CannonShellMovement : MonoBehaviour 
{
	private Vector3 Target;
	public float firingAngle = 45.0f;
	public float gravity = 9.8f;
	public GameObject cannonShellExplosion;
	PlayerHealth playerHealth;	
	//public Transform Projectile;      
	//private Transform myTransform;
	private Transform player;
	void Start()
	{
		//playerAttack = GetComponent<PlayerAttack> ();
		player=GameObject.FindGameObjectWithTag("Player").transform;
		Target =new Vector3(Random.Range(player.position.x-5,player.position.x+5),0,Random.Range(player.position.z+5,player.position.z-5));
		StartCoroutine(SimulateProjectile());
	}
	
	void OnTriggerEnter(Collider other)	
	{
		if (other.tag == "Player") 
		{
			Instantiate (cannonShellExplosion,transform.position,Quaternion.identity);
			Destroy(gameObject);
			playerHealth = other.gameObject.GetComponent<PlayerHealth>();
			playerHealth.TakeDamage(30);
		}
		else if (other.tag == "Ground")
		{
			Instantiate (cannonShellExplosion,transform.position,Quaternion.identity);
			Destroy(gameObject);
		}
	}
	
	IEnumerator SimulateProjectile()
	{
		// Short delay added before Projectile is thrown
		//yield return new WaitForSeconds(1.5f);
		
		// Move projectile to the position of throwing object + add some offset if needed.
		//Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);
		
		// Calculate distance to target
		float target_Distance = Vector3.Distance(transform.position, Target);
		
		// Calculate the velocity needed to throw the object to the target at specified angle.
		float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);
		
		// Extract the X  Y componenent of the velocity
		float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
		float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);
		
		// Calculate flight time.
		float flightDuration = target_Distance / Vx;
		// Rotate projectile to face the target.
		transform.rotation = Quaternion.LookRotation(Target - transform.position);
		
		float elapse_time = 0;
		
		while (elapse_time < flightDuration || transform.position.y>0.5f)
		{
			transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
			elapse_time += Time.deltaTime;
			yield return null;
		}
		if (transform.position.y < 0f)
			Destroy (gameObject);
	}  
}
