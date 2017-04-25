using UnityEngine;
using System.Collections;

public class PauseScreen : MonoBehaviour 
{
	public Canvas pauseCanvas;
	public Canvas optionsCanvas;
	bool cursorEnable;
	HUD hud;
	void Start () 
	{
		hud = GameObject.Find ("GameUI").GetComponent<HUD> ();
		Cursor.lockState = CursorLockMode.None;
		//Cursor.visible = false;
		Time.timeScale = 1;
	}
	//check whether esc key is pressed or not
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Escape) && optionsCanvas.enabled == false) 
		{
			if(hud.inventoryCanvas.enabled)
			{
				hud.InventoryShow();
			}
			else
			{
				cursorEnable = Cursor.visible;
				pauseCanvas.enabled = true;
				Time.timeScale=0;
				Cursor.lockState=CursorLockMode.Confined;
				Cursor.visible=true;
			}
		}
	}
	//resume the game
	public void ResumeGame()
	{
		
		Time.timeScale=1;
		Cursor.lockState=CursorLockMode.None;
		Cursor.visible=cursorEnable;
		StartCoroutine(ExecuteAfterTime(0.2f));

	}
	//select options
	public void SelectOptions()
	{
		pauseCanvas.enabled = !pauseCanvas.enabled;
		optionsCanvas.enabled = !optionsCanvas.enabled;
	}
	//exit to mainmenu
	public void ExittoMain () 
	{
		Application.LoadLevel(0);
		Time.timeScale=1;
	}
	//exit game
	public void ExitGame () 
	{
		Application.Quit ();
	}

	IEnumerator ExecuteAfterTime(float time)
	{
		yield return new WaitForSeconds(time);
		pauseCanvas.enabled = false;
	}
}
