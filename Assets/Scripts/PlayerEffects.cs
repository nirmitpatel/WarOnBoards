using UnityEngine;
using System.Collections;

public class PlayerEffects : MonoBehaviour 
{
	public GameObject healthPackCollectEffect;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("HealthPack")) 
		{
			StartCoroutine(ActivateHealEffect());
		}
	}

	IEnumerator ActivateHealEffect()
	{
		Debug.Log ("Health");
		healthPackCollectEffect.SetActive(true);
		yield return new WaitForSeconds (5);
		healthPackCollectEffect.SetActive(false);
	}

}
