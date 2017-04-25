using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIManager : MonoBehaviour 
{
	string username;
	public InputField input;
	public Text nameDisplay;
	public Canvas optionscanvas;
	//highscoreboard
	private Transform score;
	private Transform player;
	private Text scoreText;
	private Text playerText;
	//public GameObject light;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	public void NameUpdate()
	{
		if (input.text != "")
		{
			username = "Hello, " + input.text;
			nameDisplay.text = username;
			PlayerPrefs.SetString("Name", input.text);
			input.DeactivateInputField();
			//input.enabled = false;
			/*int t = 0;
			foreach (GameObject temp in GameObject.FindGameObjectsWithTag("InputField"))
			{
				Destroy(temp);
				t++;
			}
			Debug.Log(t);*/
		}
	}
	// To Start The Game
	public void StartGame()
	{
		Cursor.visible = false;
		//GameObject.DontDestroyOnLoad (light);
		Application.LoadLevel(1);

	}
	// To know which type of level is selected
	public void LevelSelected()
	{
		
	}
	//To Exit the Game
	public void Exit()
	{
		Application.Quit();
	}
	public void HighScore()
	{
		//optionscanvas.enabled = true;
		//panel1.SetActive(true);
		for (int i = 1; i <= 10; i++)
		{
			score = optionscanvas.transform.Find("HighscorePanel/" + i + "/Score/ScoreText");
			player = optionscanvas.transform.Find("HighScorePanel/" + i + "/Name/NameText");
			scoreText = score.GetComponent<Text>();
			playerText = player.GetComponent<Text>();
			if (PlayerPrefs.GetString("player" + i) != "")
			{
				scoreText.text = PlayerPrefs.GetInt("score" + i).ToString();
				playerText.text = PlayerPrefs.GetString("player" + i);
			}
			else
			{
				playerText.text = "-";
				if (PlayerPrefs.GetInt("score" + i) == 0)
					scoreText.text = "-";
			}
		}
	}
	public void DisableBoolAnimator(Animator anim)
	{
		anim.SetBool ("IsDisplayed", false);
	}
	public void EnableBoolAnimator(Animator anim)
	{
		anim.SetBool ("IsDisplayed", true);
	}
	public void ShowMenu(Animator anim)
	{
		anim.SetBool ("IsMenuDisplayable", true);
	}
	public void HideMenu(Animator anim)
	{
		anim.SetBool ("IsMenuDisplayable", false);
	}
}
