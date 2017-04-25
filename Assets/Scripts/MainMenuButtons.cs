using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MainMenuButtons : MonoBehaviour
{
    public Canvas optionscanvas;
    //highscoreboard
    private Transform score;
    private Transform player;
    private Text scoreText;
    private Text playerText;
    //highscoreboardpanel
    public GameObject panel1;
    //difficultylevelpanel
    public GameObject panel2;
    //how to play panel
    public GameObject panel3;
    //playername
    string username;
    public InputField input;
    public Text nameDisplay;
    void Start()
    {
        optionscanvas.enabled = false;
    }
    // Player Name
    public void NameUpdate()
    {
        if (input.text != "")
        {
            username = "Welcome, " + input.text;
            nameDisplay.text = username;
            PlayerPrefs.SetString("Name", input.text);
            input.enabled = false;
            int t = 0;
            foreach (GameObject temp in GameObject.FindGameObjectsWithTag("InputField"))
            {
                Destroy(temp);
                t++;
            }
            Debug.Log(t);
        }
    }
    // To Start The Game
    public void StartGame()
    {
        Application.LoadLevel("Main");
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
    public void HowToPlay()
    {
        optionscanvas.enabled = true;
        panel3.SetActive(true);
    }
    //to find the top 10 scores and show highscore panel
    public void HighScore()
    {
        optionscanvas.enabled = true;
        panel1.SetActive(true);
        for (int i = 1; i <= 10; i++)
        {
            score = optionscanvas.transform.Find("HighScorePanel/" + i + "/Score");
            player = optionscanvas.transform.Find("HighScorePanel/" + i + "/Name");
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
    // to enable the difficultypanel
    public void DifficultyLevel()
    {
        optionscanvas.enabled = true;
        panel2.SetActive(true);
    }
    // to close the panels
    public void Close()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(false);
        optionscanvas.enabled = false;
    }
}
