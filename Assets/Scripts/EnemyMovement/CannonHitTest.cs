using UnityEngine;
using System.Collections;

public class CannonHitTest : MonoBehaviour {

	// Use this for initialization
	public GameObject cannon;
	public Transform upEnd;
	public PlayerHealth playerHealth;
	public GameObject keyCup;
	public int cannon_count;
	double fireRate;
	double nextFire;
	private HUD hud;

	void Start()
	{
		hud = GameObject.FindGameObjectWithTag ("UI").GetComponent<HUD> ();

	}

	public void shoot_cannon()
	{
		if (playerHealth.currentHealth >= 50 && keyCup.activeSelf == false && hud.timer < 60 ) 
		{
			fireRate = 20;
			if(Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;
				Instantiate(cannon,upEnd.position,upEnd.rotation);
			}
		}
	else if (playerHealth.currentHealth > 50 && keyCup.activeSelf == false)
		{

			fireRate = 40;
			if(Time.time > nextFire){
				nextFire = Time.time + fireRate;
			
				Instantiate(cannon,upEnd.position,upEnd.rotation);
			}

		}

	else if (playerHealth.currentHealth > 50&& hud.Seconds<60 )
		{
			fireRate = 30;
			if(Time.time > nextFire){
				nextFire = Time.time + fireRate;
				
				Instantiate(cannon,upEnd.position,upEnd.rotation);
			}
			
		}

		else if (playerHealth.currentHealth > 50 || hud.Seconds<60 || keyCup.activeSelf==false )
		{
			fireRate = 60;
			if(Time.time > nextFire){
				nextFire = Time.time + fireRate;
				
				Instantiate(cannon,upEnd.position,upEnd.rotation);
			}
			
		}

	}

}





