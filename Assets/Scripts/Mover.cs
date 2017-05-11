using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{
	private Vector3 currentLocation;
	public float speed;
	private Rigidbody bullet;
	public GameObject hitPrefab;
	public Transform gunBarrel;
	EnemyHealth enemyHealth;
	float variation = 0.005f;
	//public GameObject bulletExplosion;

	void Awake()
	{
		currentLocation = transform.position;
	}
	void Start () 
	{
		bullet = GetComponent<Rigidbody> ();
		bullet.velocity = (transform.forward + new Vector3(Random.Range(-variation,variation),
		                                                   Random.Range(-variation,variation),
		                                                   Random.Range(-variation,variation))) * speed;
	}
	void Update()
	{
		if (Vector3.Distance (currentLocation, transform.position) > 400)
			Destroy (gameObject);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag != "Player" && isNotEnemySphereCollider(other)) 
		{
			//Debug.Log("DEBUGG"+other);
			RaycastHit hit = new RaycastHit ();
			GameObject gunBarrel = GameObject.Find ("GunBarrel");
			Ray ray = new Ray (gunBarrel.transform.position, transform.TransformDirection (Vector3.forward));
			if (Physics.Raycast (ray, out hit, 400f)) 
			{
				if(hit.collider.tag!="Bullet")
				{
					Instantiate (hitPrefab, hit.point, Quaternion.FromToRotation (Vector3.forward, hit.normal));
				}
			}
			if (other.tag == "Enemy") 
			{
				enemyHealth = other.gameObject.GetComponentInChildren<EnemyHealth>();
				enemyHealth.TakeDamage(10);
			}
			Destroy (gameObject);
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
