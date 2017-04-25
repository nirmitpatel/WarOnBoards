using UnityEngine;
using System.Collections;

public class E_Mover : MonoBehaviour {

	// Use this for initialization
	private Transform player;  
	private PlayerHealth playerHealth;
	public GameObject hitPrefab;
	public Transform gunbarrel;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		playerHealth = player.GetComponent<PlayerHealth>();

	}
	void OnTriggerEnter(Collider other)

	{
	if (other.tag == "Player") 
		{
			RaycastHit hit = new RaycastHit ();
			GameObject gunBarrel = GameObject.Find ("GunBarrel");
			Ray ray = new Ray (gunBarrel.transform.position, transform.TransformDirection (Vector3.forward));
			if (Physics.Raycast (ray, out hit, 100f)) 
			{
				Instantiate (hitPrefab, hit.point, Quaternion.FromToRotation (Vector3.forward, hit.normal));
			}
			playerHealth.TakeDamage(1);
			Destroy(gameObject);
		
		}

	}

}
