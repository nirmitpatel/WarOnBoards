using UnityEngine;
using System.Collections;

public class HealthPackSpawning : MonoBehaviour {

	// Use this for initialization
	private Vector3 spawnPoint;
	public GameObject healthPack;
	private PlayerHealth playerHealth;
	public Transform player;
	private EnemySight enemySight;
	private DoneLastPlayerSighting dlps;
	bool hflag=false;
	bool pflag= false;
	public GameObject keyCup;
	// Update is called once per frame

	void Start()
	{
		dlps = GameObject.FindGameObjectWithTag ("GameController").GetComponent<DoneLastPlayerSighting> ();
		playerHealth = player.GetComponent<PlayerHealth> ();
	//	enemySight = enemy.GetComponent<EnemySight> ();
	}
	void Update () 
	{
		if (playerHealth.currentHealth <= 40 && playerHealth.currentHealth!=0 && hflag==false) 
		{
			SpawnH();
		}
		if (dlps.death_count >= 2 && pflag == false) {
			SpawnK();
		}
	}

	void SpawnH()
	{
		spawnPoint =new Vector3(Random.Range(player.position.x-10,player.position.x+10),1.1f,Random.Range(player.position.z-10,player.position.z+10));
		Instantiate(healthPack,spawnPoint,Quaternion.Euler(270,0,0));
		hflag = true;
	}

	void SpawnK()
	{
		spawnPoint =new Vector3(Random.Range(0,500),0,Random.Range(0,500));
		Instantiate(keyCup,spawnPoint,Quaternion.identity);
		pflag = true;
	}
}

