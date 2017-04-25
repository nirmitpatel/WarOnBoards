using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;
public class InventoryDB : MonoBehaviour 
{
	private List<Item1> database = new List<Item1> ();
	private JsonData itemData;

	void Start()
	{
		itemData = JsonMapper.ToObject (File.ReadAllText(Application.dataPath+"/StreamingAssets/Items.json"));
		ConstructItemsDatabase ();
	}
	void ConstructItemsDatabase()
	{
		for (int i=0; i<itemData.Count; i++) 
		{
			database.Add(new Item1((int)itemData[i]["id"],itemData[i]["title"].ToString(),itemData[i]["description"].ToString(),(int)itemData[i]["itemstobeadded"],(int)itemData[i]["range"],(int)itemData[i]["damage"],itemData[i]["slug"].ToString()));
		}
	}
	public Item1 FetchItembyID(int id)
	{
		for (int i=0; i<database.Count; i++) 
		{
			if(database[i].ID == id)
				return database[i];
		}
		return null;
	}
}
public class Item1
{
	public int ID{ get; set;}
	public string Title { get; set;}
	public string Description { get; set;}
	public int ItemstobeAdded { get; set;}
	public int Range { get; set;}
	public int Damage { get; set;}
	public string Slug{get;set;}
	public Sprite Sprite { get; set;}
	public Item1(int id,string title,string description ,int itemstobeadded,int range,int damage ,string slug)
	{
		this.ID = id;
		this.Description = description;
		this.Title = title;
		this.ItemstobeAdded = itemstobeadded;
		this.Range = range;
		this.Damage = damage;
		this.Slug = slug;
		this.Sprite = Resources.Load<Sprite>("Items/"+slug);
	}
	public Item1()
	{
		this.ID = -1;
	}
}