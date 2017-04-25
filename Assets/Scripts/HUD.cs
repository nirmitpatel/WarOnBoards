using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HUD : MonoBehaviour 
{
	//playername
	public Text PlayerName;
	//timer
	public Text TimerText;
	public int Minutes;
	public int Seconds;
	public float timer;
	private bool timeStarted;
	//health
	public RectTransform healthTransform;
	private float cachedY;
	private float minXValue;
	private float maxXValue;
	private int currentHealth;
	public int maxHealth;
	public Image visualHealth;
	//score
	public int score;
	public Text ScoreText;
	//pickup Indicator
	public Image Powerup;
	public Canvas inventoryCanvas;
	//GameOver
	public Canvas GameOver;
	public Text GameOverScore;
	public Text message;
	//minimap
	public GameObject Minimap;
	public PauseScreen pauseScreen;
	int mapFlag;
	public AudioSource timerSound;
	bool cursorVisible;

	void Start () 
	{
		//PlayerPrefs.SetString("Name","Ravi");
		PlayerName.text=PlayerPrefs.GetString ("Name");
		timer = (Minutes * 60) + Seconds;
		timeStarted = true;
		if (!(PlayerPrefs.HasKey ("player1"))) 
		{
			for(int i=1;i<=10;i++)
			{
				PlayerPrefs.SetString("player"+i,"");
				PlayerPrefs.SetInt("score"+i,0);
			}
		}
		cachedY = healthTransform.position.y;
		maxXValue = healthTransform.position.x;
		minXValue = healthTransform.position.x - healthTransform.rect.width;
		currentHealth = maxHealth;
		inventoryCanvas.enabled=false;
		GameOver.enabled = false;
		mapFlag=0;
	}
	//health
	private int CurrentHealth
	{
		get{return currentHealth;}
		set
		{
			currentHealth=value;
			HandleHealth();
		}
	}
	//health
	private void HandleHealth()
	{
		float currentXValue = MapValues (currentHealth,0,maxHealth,minXValue,maxXValue);
		healthTransform.position = new Vector3 (currentXValue, cachedY);
		if (currentHealth > maxHealth / 2) 
		{
			visualHealth.color=new Color32((byte)MapValues (currentHealth,maxHealth/2,maxHealth,255,0),255,0,255);
		}
		else
		{
			visualHealth.color=new Color32(255,(byte)MapValues (currentHealth,0,maxHealth/2,0,255),0,255);
		}
	}
	//health
	private float MapValues(float x,float inMin,float inMax,float outMin,float outMax)
	{
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
	//health
	public void ChangeHealth(int value)
	{
		CurrentHealth = value;
		if (value == 0) 
		{
			showGameOver(1);
		}
	}
	//score
	public void UpdateScore(int value)
	{
		score += value;
		ScoreText.text = score.ToString ();
	}
	//show indicator
	public void ShowPickupIndicator()
	{
		Powerup.enabled=true;
	}
	//turn off indicator
	public void DisableIndicator()
	{
		Powerup.enabled = false;
	}
	//timer
	private void Timer()
	{
		if (timer > 0 && timeStarted == true) {
			timer -= Time.deltaTime;
			Minutes = (int)Mathf.Floor (timer / 60);
			Seconds = (int)timer % 60;
			string m, s;
			m = Minutes.ToString ();
			s = Seconds.ToString ();
			if (Minutes < 0) {
				m = "0";
			}
			if (Minutes < 10) {
				m = "0" + m;
			}
			if (Seconds < 10) {
				s = "0" + s;
			}
			if (timer > 30) {
				TimerText.text = "TIMER\n" + m + ":" + s;
			} else {
				if (!timerSound.isPlaying)
					timerSound.Play ();
				TimerText.color = Color.red;
				TimerText.text = "TIMER\n" + m + ":" + s;
			}
		} 
		else 
		{
			showGameOver(0);
		}
	}
	public void InventoryShow()
	{
		if (!pauseScreen.pauseCanvas.enabled && !inventoryCanvas.enabled) 
		{
			cursorVisible = Cursor.visible;
			if(!Cursor.visible)
				Cursor.visible = true;
			inventoryCanvas.enabled = true;
		} 
		else if (inventoryCanvas.enabled) 
		{
			if(!cursorVisible)
				Cursor.visible = false;
			inventoryCanvas.enabled = false;
		}
//		if (inventoryCanvas.enabled) 
//		{
//			Cursor.visible = true;
//			//Cursor.lockState=CursorLockMode.Confined;
//			//Time.timeScale=0;
//		}
//		else 
//		{
//			//Time.timeScale=1;
//			Cursor.visible = false;
//			//Cursor.lockState=CursorLockMode.None;
//		}
			
	}
	public void showGameOver(int i)
	{
		cursorVisible = true;
		Time.timeScale=0;
		Cursor.lockState=CursorLockMode.Confined;
		GameOver.enabled = true;
		GameOverScore.text = "Score: "+score.ToString();
		if (i==0) 
		{
			message.text = "Time Over! Want to Replay the Level Again? ";
		}
		else if (i==1) 
		{
			message.text = "You are Killed! Want to Replay the Level Again?";
		}
		else 
		{
			message.text = "Congratulations!! Want to Replay the Level Again?";
		}
	}
	public void LoadMainMenu()
	{
		Application.LoadLevelAdditive (0);
	}
	public void LoadLevel()
	{
		Application.LoadLevelAdditive(1);
	}
	void showChessBoard()
	{
		if (mapFlag == 0) 
		{
			Minimap.SetActive (true);
			mapFlag=1;
		}
		else
		{
			Minimap.SetActive (false);
			mapFlag=0;
		}

	}
	void Update()
	{
		Timer ();
		if (Input.GetKeyDown (KeyCode.Q))
		{
			showChessBoard();
		}

		if (Input.GetKeyDown (KeyCode.E))
		{
			ShowPickupIndicator();
		}
		if (Input.GetKeyDown (KeyCode.R))
		{
			DisableIndicator();
		}
		if (Input.GetKeyDown (KeyCode.V)) 
		{
			InventoryShow();
		}
	}
}
