using UnityEngine;
using System.Collections;
using System.Threading;

public class PlayerAttack : WeaponSystems
{
	public GameObject cannon;
	private float nextFire_CannonShell;
	public float cannonShellFireRate;
	private RaycastHit hit_cannon;
	public GameObject explosion_CannonshellLaunch;
	public static RaycastHit floorHit;
	Rigidbody shellInstance;
	public float minLaunchForce = 15f;
	public float maxLaunchForce = 30f;
	public float maxChargeTime = 1f;
	private float currentLaunchForce;
	private float chargeSpeed;
	private bool cannonFired;
	public Transform upEnd;
	public AudioClip CannonShellLaunchSound;
	
	public GameObject heatSeekingMissile;
	public Transform HSM_SpawnPoint;
	private float nextFire_HSM;
	public float heatSeekingMissileFireRate;
	public GameObject target;
	private RaycastHit hit;
	
	public GameObject bullet;
	public Transform gunBarrel;
	private float nextFire_Bullet;
	public float gunFireRate;
	public GameObject GunFireExplosion;
	public AudioClip gunFireSound;
	
	public GameObject mine;
	private float nextDrop_Mine;
	public Transform mineDropPoint;
	public float mineDropRate;
	
	public GameObject laserGuidedBomb;
	private float nextFire_LGBomb;
	public float LGBombFireRate;
	private RaycastHit hit_lgbomb;
	private Vector3 LGBSpawnPoint;
	
	public GameObject timeReleaseBouncingShell;
	private float tRBS_Activetime;

	private Vector2 cursorHotspot;
	Ray CamRay;
	int layerMask; 
	//	public GameObject flare;
	//	public Transform flarePoint1;
	//	public Transform flarePoint2;
	//	public Transform flarePoint3;
	//	public Transform flarePoint4;
	public Texture2D cursorTexture;
	HUD hud;
	public PauseScreen pauseScreen;
	NewInventory inventory;

	void Awake()
	{
		//Cursor.visible = false;
	}

	void OnEnable()
	{
		currentLaunchForce = minLaunchForce;
	}
	
	void Start()
	{
		chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
		layerMask =  1 << 12;
		hud = GameObject.Find ("GameUI").GetComponent<HUD> ();
		inventory = GameObject.FindGameObjectWithTag ("UI").GetComponent<NewInventory> ();
	}
	
	void Update() 
	{	
		if(!hud.inventoryCanvas.enabled && !pauseScreen.pauseCanvas.enabled)
		{
			if (currentWeapon == 1) 
			{
				CamRay = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast (CamRay,out hit,150,layerMask)) 
				{ 
					target = hit.collider.gameObject;
					if(hit.collider.tag.Equals("Enemy") && Vector3.Angle(transform.forward,(target.transform.position-transform.position).normalized)<80f)
					{
						cursorHotspot = new Vector2(256/2,256/2);
						Cursor.SetCursor(cursorTexture,cursorHotspot,CursorMode.Auto);
						if(Input.GetButton("Fire1") && inventory.ItemAmount(currentWeapon)!=0 && Time.time>nextFire_HSM)
						{
							HSM_Launch(target);
						}
					}
					else
						Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); 
				}
				else
					Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); 
			}
			else if(currentWeapon == 3)
			{
				if(Time.time > nextFire_CannonShell)
				{
					if (currentLaunchForce >= maxLaunchForce && !cannonFired)
					{
						currentLaunchForce = maxLaunchForce;
						Fire ();
					}
					else if (Input.GetButtonDown("Fire1"))
					{
						cannonFired = false;
						currentLaunchForce = minLaunchForce;
						
						//m_ShootingAudio.clip = m_ChargingClip;
						//m_ShootingAudio.Play ();
					}
					else if (Input.GetButton ("Fire1") && !cannonFired)
					{
						currentLaunchForce += chargeSpeed * Time.deltaTime;
					}
					else if (Input.GetButtonUp ("Fire1") && !cannonFired)
					{
						Fire ();
					}
				}
			}
			else if (Input.GetButton ("Fire1") && (currentWeapon!=1 && currentWeapon!=3) && inventory.ItemAmount(currentWeapon)!=0) 
			{
				//			string result = inventory.RemoveItem(currentWeapon);
				//			if(result == "yes")
				//			{
				switch (currentWeapon) 
				{
				case 0: 
					GunFire(); 
					break;
					//					case 3:
					//						CannonShellFire();
					//						break;
				case 2:
					DropMine();
					break;
				case 4:
					if(Time.time>nextFire_LGBomb)
					{
						inventory.RemoveItem(currentWeapon);
						nextFire_LGBomb = Time.time + LGBombFireRate;
						StartCoroutine(activateLaserGuidedBomb());
					}
					break;
				default:
					break;
				}
				//			}
			}
		}
		//		CamRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		//		if (Physics.Raycast (CamRay,out hit,150,layerMask)) 
		//		{ 
		//			if(hit.collider.tag.Equals("Enemy"))
		//			{
		//				Cursor.SetCursor(cursorTexture,Vector2.one,CursorMode.Auto);
		//				if(Input.GetButton("Jump") && Time.time>nextFire_HSM)
		//				{
		//					HSM_Launch();
		//				}
		//			}
		//			else
		//				Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); 
		//		}
		//		else
		//			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); 
		//
		//		if(Input.GetKeyDown("3"))
		//		{
		//			if(Physics.Raycast(CamRay,out hit_lgbomb,200))
		//			{
		//				LGBSpawnPoint = hit_lgbomb.point;
		//				StartCoroutine(activateLaserGuidedBomb());
		//			}
		//		}
		//		
		//		if(Input.GetButton("Fire1") && Time.time>nextFire_CannonShell)
		//		{
		//			if(Physics.Raycast(CamRay,out hit_cannon,200))
		//			{
		//				CannonShellFire(hit_cannon);
		//			}
		//		}
		//		
		//		if (Input.GetButton ("Fire2") && Time.time>nextFire_Bullet) 
		//		{
		//			GunFire();
		//		}
		//		if (Input.GetKeyDown ("5")) 
		//		{
		//			Instantiate(mine,mineDropPoint.position,mineDropPoint.rotation);
		//		}
		////		if (Input.GetKey("1")) 
		////		{
		////			Instantiate(flare,flarePoint1.position,flare.transform.rotation);
		////			Instantiate(flare,flarePoint2.position,flare.transform.rotation);
		////			Instantiate(flare,flarePoint3.position,flare.transform.rotation);
		////			Instantiate(flare,flarePoint4.position,flare.transform.rotation);
		////		}
		//		if (!(Time.time>tRBS_Activetime) || isShellToStart()) 
		//		{ 
		//
		//			timeReleaseBouncingShell.SetActive(true);
		//		}
		//		else
		//			timeReleaseBouncingShell.SetActive(false);
	}
	
	enum WeaponID
	{
		MachineGun,
		Missile,
		Mine,
		CannonShell,
		LaserGuidedBomb
	};
	
	bool isShellToStart()
	{
		if (Input.GetKeyDown ("2")) 
		{
			tRBS_Activetime = Time.time + 5f;
			return true;
		}
		else
			return false;
	}
	
	IEnumerator activateLaserGuidedBomb()
	{
		CamRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (CamRay, out hit_lgbomb, 200)) 
		{
			for (int i=0; i<10; i++) 
			{
				LGBSpawnPoint = hit_lgbomb.point;
				Instantiate (laserGuidedBomb, new Vector3 (Random.Range (LGBSpawnPoint.x - 20, LGBSpawnPoint.x + 20), 100, Random.Range (LGBSpawnPoint.z - 20, LGBSpawnPoint.z + 20)), Quaternion.identity);
				yield return new WaitForSeconds (0.5f);
			}
		}
	}
	
	void GunFire()
	{
		if (Time.time > nextFire_Bullet) 
		{
			nextFire_Bullet = Time.time + gunFireRate;
			Instantiate (bullet, gunBarrel.position, gunBarrel.rotation);
			Instantiate (GunFireExplosion, gunBarrel.position, gunBarrel.rotation);
			AudioSource.PlayClipAtPoint (gunFireSound,gunBarrel.position);
		}
	}
	
	void HSM_Launch(GameObject d_target)
	{
		inventory.RemoveItem (currentWeapon);
		nextFire_HSM = Time.time + heatSeekingMissileFireRate;
		GameObject temp=Instantiate(heatSeekingMissile,HSM_SpawnPoint.position,HSM_SpawnPoint.rotation) as GameObject;
		//temp.name="hs";
		//GameObject.Find("hs").GetComponent<HeatSeekingMissile>().setTarget(d_target);
		temp.GetComponent<HeatSeekingMissile>().setTarget(d_target);
	}
	
	//	void CannonShellFire()
	//	{
	//		CamRay = Camera.main.ScreenPointToRay(Input.mousePosition);
	//		if (Physics.Raycast (CamRay, out hit_cannon, 200)) 
	//		{
	//			if (Time.time>nextFire_CannonShell && Vector3.Angle (transform.forward, (hit_cannon.point - gunBarrel.transform.position).normalized) < 80f) 
	//			{
	//				inventory.RemoveItem(currentWeapon);
	//				floorHit = hit_cannon;
	//				nextFire_CannonShell = Time.time + cannonShellFireRate;
	//				if (Vector3.Distance (hit_cannon.point, transform.position) <= 100) 
	//				{
	//					Instantiate (cannon, upEnd.position, upEnd.rotation);
	//				} 
	//				else 
	//				{
	//					Vector3 launchDirection = hit_cannon.point - upEnd.transform.position;
	//					hit_cannon.point = upEnd.transform.position + launchDirection.normalized * 100;
	//					Instantiate (cannon, upEnd.position, upEnd.rotation);
	//				}
	//				Instantiate (explosion_CannonshellLaunch, upEnd.position, upEnd.rotation);
	//			}
	//		}
	//	}
	
	private void Fire ()
	{
		nextFire_CannonShell = Time.time + cannonShellFireRate;
		cannonFired = true;
		
		GameObject cannonShell =
			Instantiate (cannon, upEnd.position, upEnd.rotation) as GameObject;
		Instantiate (explosion_CannonshellLaunch, upEnd.position, upEnd.rotation);
		AudioSource.PlayClipAtPoint (CannonShellLaunchSound, upEnd.position,50.0f);
		
		shellInstance = cannonShell.GetComponent<Rigidbody> ();
		
		shellInstance.velocity = currentLaunchForce * (upEnd.forward  + new Vector3(0f,0.1f,0f));
		
		//		m_ShootingAudio.clip = m_FireClip;
		//		m_ShootingAudio.Play ();
		
		currentLaunchForce = minLaunchForce;
	}
	
	void DropMine()
	{
		if(Time.time>nextDrop_Mine)
		{
			inventory.RemoveItem(currentWeapon);
			nextDrop_Mine = Time.time + mineDropRate;
			Instantiate(mine,mineDropPoint.position,mineDropPoint.rotation);
		}
	}
}
