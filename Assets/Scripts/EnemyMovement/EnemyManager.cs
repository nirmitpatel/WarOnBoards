using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public PlayerHealth playerHealth;
	public GameObject enemy;
	public float spawnTime = 20f;
	private Vector3 spawnPoint;
	public int enemycount;
	Radar radar;
	GameObject player;
	public int start_count ; 
	void Start ()
	{
		enemycount= GameObject.FindGameObjectsWithTag("Enemy").Length;
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		radar = player.GetComponent<Radar>();
	}
	void Update()
	{
		spawnPoint=new Vector3(Random.Range(0.0F,500.0F), 0, Random.Range(0.0F, 500.0F));
		
	}
	
	void Spawn ()
	{
		if (playerHealth.currentHealth <= 0f) {
			return;
		}
		
		//int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		else if (enemycount >= 10) {
			
			return;
		}
		else {
			GameObject enemy1 = Instantiate (enemy, spawnPoint,Quaternion.identity )as GameObject;
			radar.AddEnemy (enemy1);
			enemycount++;
		}
	}
}
