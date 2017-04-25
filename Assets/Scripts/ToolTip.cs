using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ToolTip : MonoBehaviour 
{
	private Item1 item;
	private string data;
	private GameObject tooltip;
	void Start()
	{
		tooltip = GameObject.Find ("ToolTip");
		tooltip.SetActive (false);
	}
	void Update()
	{
		if (tooltip.activeSelf) 
		{
			tooltip.transform.position=Input.mousePosition;
		}
	}
	public void Activate(Item1 item)
	{
		this.item = item;
		ConstructDataString ();
		tooltip.SetActive (true);
	}
	public void Deactivate()
	{
		tooltip.SetActive (false);
	}
	public void ConstructDataString()
	{
		data = "<color=#000000><b>"+item.Title+"</b></color>\n<color=blue>"+item.Description+"</color>\n<color=green>Damage:  "+item.Damage+"</color>\n<color=brown>Range:  "+item.Range+"</color>";
		tooltip.transform.GetChild (0).GetComponent<Text> ().text = data;
	}
}
