using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CupCollect : MonoBehaviour
{
	public GameObject KeyCup;
	public Text Score;
	public int score;
	public AudioClip pickUpCollectSound;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			AudioSource.PlayClipAtPoint(pickUpCollectSound,transform.position);
			KeyCup.SetActive(false);
			score+=10;
			Score.text=score.ToString();
		}
	}
}
