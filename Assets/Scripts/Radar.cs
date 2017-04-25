using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Radar : MonoBehaviour 
{
	public List<GameObject>trackedObjects;
	List<GameObject>radarObjects;
	List<GameObject>borderObjects;
	public GameObject radarPrefab;
	public float switchDistance;
	public Transform helpTransform;
	// Use this for initialization
	void Start () 
	{
		//trackedObjects = new List<GameObject> ();
		radarObjects = new List<GameObject> ();
		borderObjects = new List<GameObject> ();
		CreateRadarObjects ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		for (int i=0; i<radarObjects.Count; i++) 
		{
			if(radarObjects[i]!= null)
			{
				if(Vector3.Distance(radarObjects[i].transform.position,transform.position)>switchDistance)
				{
					helpTransform.LookAt(radarObjects[i].transform);
					borderObjects[i].transform.position=transform.position + switchDistance * helpTransform.forward;
					borderObjects[i].layer=LayerMask.NameToLayer("Radar");
					radarObjects[i].layer=LayerMask.NameToLayer("Invisible");
				}
				else
				{
					radarObjects[i].layer=LayerMask.NameToLayer("Radar");
					borderObjects[i].layer=LayerMask.NameToLayer("Invisible");
				}
			}
		}
	}
	public void AddEnemy(GameObject enemy)
	{
		trackedObjects.Add(enemy);
		GameObject k=Instantiate (radarPrefab,enemy.transform.position,Quaternion.identity)as GameObject;
		k.transform.SetParent(enemy.transform);
		radarObjects.Add(k);
		GameObject j=Instantiate (radarPrefab,enemy.transform.position,Quaternion.identity)as GameObject;
		j.transform.SetParent(enemy.transform);
		borderObjects.Add(j);

	}
	void CreateRadarObjects()
	{
		foreach (GameObject o in trackedObjects) 
		{
			GameObject k=Instantiate (radarPrefab,o.transform.position,Quaternion.identity)as GameObject;
			k.transform.SetParent(o.transform);
			radarObjects.Add(k);
			GameObject j=Instantiate (radarPrefab,o.transform.position,Quaternion.identity)as GameObject;
			j.transform.SetParent(o.transform);
			borderObjects.Add(j);
		}
	}
}
