using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour 
{
	public GameObject KeyCup;
	public Text score; 
	int value1,value2,prevScore;
	string name1,name2;
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			if(KeyCup.activeSelf==false)
			{
				int s=int.Parse(score.text);
				int c=PlayerPrefs.GetInt("score10");
				if(s>c)
				{
					int i;
					for(i=1;i<=9;i++)
					{
						prevScore=PlayerPrefs.GetInt("score"+i);
						if(s>prevScore)
							break;
					}
					for(int j=9;j>=i;j--)
					{
						PlayerPrefs.SetString("player"+(j+1),PlayerPrefs.GetString("player"+j));
						PlayerPrefs.SetInt("score"+(j+1),PlayerPrefs.GetInt("score"+j));

						/*value1=PlayerPrefs.GetInt("score"+(j+1));
						value2=PlayerPrefs.GetInt("score"+j);
						if(value1>=value2)
						{
							name1=PlayerPrefs.GetString("player"+(j+1));
							name2=PlayerPrefs.GetString("player"+j);

							PlayerPrefs.SetString("player"+(j+1),name2);
							PlayerPrefs.SetInt("player"+j,value1);
							PlayerPrefs.SetInt("player"+(j+1),value2);
						}*/

					}
					PlayerPrefs.SetString("player"+i,PlayerPrefs.GetString("Name"));
					PlayerPrefs.SetInt("score"+i,s);
				}
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}
}