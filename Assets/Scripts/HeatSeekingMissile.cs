using UnityEngine;
using System.Collections;

public class HeatSeekingMissile : MonoBehaviour 
{
	Vector3 rotationOfHSM;
	GameObject target;
	public float speed = 5f;
	float ease = 5f;
	float tempDistance;
	int i;
	public GameObject smoke;
	GameObject nozzle;
	public GameObject explosion_HSM;
	EnemyHealth enemyHealth;
	float findClosestTargetStartTime;
	bool isFindCLosestTargetStarted=false;
	///public AudioClip HSMHitSound;

	void Start()
	{
		findClosestTargetStartTime = Time.time + 1.0f;
	}	
	public void setTarget(GameObject target)
	{
		this.target = target;
	}

	void Update()
	{
		if (!isFindCLosestTargetStarted && Time.time > findClosestTargetStartTime) 
		{
			StartCoroutine (findClosestTarget ());
			isFindCLosestTargetStarted = true;
		} 
		//rotate to look at the player
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), ease * Time.deltaTime);
		//move towards the player
		transform.position += transform.forward * Time.deltaTime * speed;
		nozzle = GameObject.Find("nozzleFlame");
		Instantiate (smoke, nozzle.transform.position, nozzle.transform.rotation);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy" && !other.ToString ().Contains ("Sphere")) 
		{
			Destroy (gameObject);
			//AudioSource.PlayClipAtPoint(HSMHitSound,transform.position);
			Instantiate(explosion_HSM,transform.position,transform.rotation);
			enemyHealth = other.gameObject.GetComponentInChildren<EnemyHealth>();
			enemyHealth.TakeDamage(60);
		}
	}

	IEnumerator findClosestTarget()
	{
		GameObject closestTarget = target;
		float distance;
		while (true) 
		{
			distance = Vector3.Distance(transform.position,target.transform.position);
			GameObject[] targets = GameObject.FindGameObjectsWithTag ("Enemy");
			for (i=0; i<targets.Length; i++) 
			{
				tempDistance = Vector3.Distance (transform.position, targets [i].transform.position);
				if(tempDistance < distance && Vector3.Angle(transform.forward,(targets[i].transform.position - transform.position).normalized)<50f)
				{
					distance = tempDistance;
					closestTarget = targets[i];
				}
			}
			target = closestTarget;
			yield return null;
		}
	}
}
