using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;	
public class NewInventory : MonoBehaviour 
{
	GameObject inventoryPanel;
	GameObject slotPanel;
	public GameObject inventorySlot;
	public GameObject inventoryItem;
	InventoryDB database;
	int slotAmount;
	public List<Item1> items =new List<Item1>();
	public List<GameObject> slots = new List<GameObject> ();

	void Start()
	{
		database = GetComponent<InventoryDB> ();
		slotAmount = 6;
		inventoryPanel=GameObject.Find("Inventory Panel");
		slotPanel = inventoryPanel.transform.FindChild ("Slot Panel").gameObject;
		for (int i=0; i<slotAmount; i++) 
		{
			items.Add(new Item1());
			slots.Add(Instantiate(inventorySlot));
			slots[i].transform.SetParent(slotPanel.transform);
		}
		AddItem (0);
		AddItem (1);
		AddItem (2);
		AddItem (3);
		AddItem (4);
		InitializeStartItems ();
	}
	void InitializeStartItems()
	{
		Item1 itemToInitialize;
		for (int i=0; i<5; i++) 
		{
		 	itemToInitialize = database.FetchItembyID (i);
			int value = itemToInitialize.ItemstobeAdded;
			for(int j=0 ; j<value ; j++)
			{
				AddItem(i);
			}
		}

	}
	public void AddItem(int id)
	{
		Item1 itemToAdd = database.FetchItembyID (id);
		for (int i=0; i<items.Count; i++) 
		{
			if(items[i].ID==-1)
			{
				items[i]=itemToAdd;
				GameObject itemObj=Instantiate(inventoryItem);
				itemObj.GetComponent<ItemData>().item=itemToAdd;
				itemObj.transform.SetParent(slots[i].transform);
				itemObj.transform.position=Vector2.zero;
				itemObj.GetComponent<Image>().sprite=itemToAdd.Sprite;
				itemObj.name=itemToAdd.Title+" Image";
				slots[i].name=itemToAdd.Title+" Slot";
				break;
			}
			else if(items[i].ID == id)
			{
				ItemData data = slots [i].transform.GetChild (0).GetComponent<ItemData> ();
				data.amount++;
				data.transform.GetChild (0).GetComponent<Text> ().text = data.amount.ToString ();
				break;
			}
		}
	}
	public string RemoveItem(int id)
	{
		int pos=0;
		for (int i=0; i<items.Count; i++) 
		{
			if(items[i].ID==id)
			{
				pos=i;
				break;
			}

		}
		ItemData data = slots[pos].transform.GetComponentInChildren<ItemData>();
		if (data.amount != 0) 
		{
			data.amount--;
			data.transform.GetComponentInChildren<Text>().text = data.amount.ToString();
			return "yes";
		} 
		else 
		{
			Debug.Log("You dont have any "+items[pos].Title+" explore the map to find more or choose another weapon");
			return "no";
		}
	}
	public int ItemAmount(int id)
	{
		int pos=0;
		for (int i=0; i<items.Count; i++) 
		{
			if(items[i].ID==id)
			{
				pos=i;
				break;
			}
			
		}
		ItemData data = slots[pos].transform.GetComponentInChildren<ItemData>();
		return data.amount;
	}
}
