using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {
	public AudioClip shotClip;
	public GameObject mine;
	public Transform mineDropPoint;
	private   Transform Player;
	public  Transform enemy;
	private bool  IsAttacking= false;
	public Rigidbody  Bullet;
	public  Transform SpawnPoint ;
	private Vector3 Distance ;
    float DistanceFrom ;
	public    double fireRate = 10;
	public double nextFire = 0;
	public Rigidbody Shoot;
	public	int minecount = 0;
	private CannonHitTest cannonhit;
	private PlayerHealth playerHealth;
	float ease= 5f;
	private UnityEngine.NavMeshAgent nav;
	void Start()
	{
		nav = GetComponent<UnityEngine.NavMeshAgent> ();
		Player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		playerHealth = Player.GetComponent<PlayerHealth> ();
	}
	void Update(){

		Attacking ();
		// Calculate the distance between the player  the enemy
		if (playerHealth.currentHealth > 0) {
			Distance = (enemy.position - Player.position);
			Distance.y = 0;
			DistanceFrom = Distance.magnitude;
			Distance /= DistanceFrom;
		
			if (DistanceFrom < 50) {
				IsAttacking = true;
			} else {
				IsAttacking = false;
			}
		}
	}
	void Attacking(){
		if(IsAttacking){
			

			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.position - transform.position), ease * Time.deltaTime);

			//Shoot
			nav.Stop();
			if(Time.time > nextFire && playerHealth.currentHealth>0){
				nextFire = Time.time + fireRate;
				
				 Shoot =(Rigidbody) Instantiate(Bullet,SpawnPoint.position,SpawnPoint.rotation);
				Shoot.AddForce(SpawnPoint.forward*5000);
				AudioSource.PlayClipAtPoint(shotClip,SpawnPoint.position );
			}
		}
	}

	public void DropMine()
	{
		Instantiate (mine, mineDropPoint.position, mineDropPoint.rotation);
		minecount += 1;
	}


}
