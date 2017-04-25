using UnityEngine;

public class EnemyManager2 : MonoBehaviour
{
	public PlayerHealth playerHealth;
	public GameObject enemy;
	private Vector3 spawnPoint;
	bool spawnmastertank; 
	private EnemyManager enemymanager;
	public GameObject KeyCup;
	Radar radar;
	GameObject player;
	public AudioClip masterTankSpawnSound;

	void Start ()
	{
		spawnmastertank = false;
		enemymanager = GetComponent<EnemyManager> ();
		player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		radar = player.GetComponent<Radar>();

	}
	void Update()
	{
		spawnPoint =new Vector3(Random.Range(0.0F,500.0F), 0, Random.Range(0.0F, 500.0F));
				
		if (KeyCup.activeSelf == false && spawnmastertank == false) {
			Spawn ();
			spawnmastertank = true;
		} else if (enemymanager.enemycount == 0) {
			
			Spawn ();
			spawnmastertank=true;
		}
		
		
	}
	
	void Spawn ()
	{
		if(playerHealth.currentHealth <= 0f)
		{
			return;
		}
		AudioSource.PlayClipAtPoint (masterTankSpawnSound, spawnPoint);
		//int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		GameObject enemy1 = Instantiate (enemy, spawnPoint,Quaternion.identity ) as GameObject;
		radar.AddEnemy (enemy1);
	}
}
