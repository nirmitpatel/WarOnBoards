using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemData : WeaponSystems,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler 
{
	public Item1 item;
	public int amount;
	private ToolTip tooltip;
	private NewInventory inv;
	HUD hud;
	GameObject crossHairSprite;

	void Start()
	{
		inv = GameObject.Find ("GameUI").GetComponent<NewInventory> ();
		hud = GameObject.Find ("GameUI").GetComponent<HUD> ();
		tooltip = inv.GetComponent<ToolTip> ();
		crossHairSprite = GameObject.FindGameObjectWithTag ("CrossHair");
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		if(item!=null)
			tooltip.Activate (item);
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		if (item != null) 
		{
			//Debug.Log("You selected item"+item.Title);
			currentWeapon = item.ID;
			StartCoroutine(ExecuteAfterTime(0.2f));
			//inv.RemoveItem(item.ID);
		}
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		tooltip.Deactivate();
	}

	IEnumerator ExecuteAfterTime(float time)
	{
		yield return new WaitForSeconds(time);
		hud.InventoryShow ();
		SetCursor(currentWeapon);
	}

	void SetCursor(int currWeap)
	{
		switch (currWeap) 
		{
		case 0:
			Cursor.visible = false;
			ActivateCrossHairSprite();
			break;
		case 1:
			Cursor.visible = true;
			DeactivateCrossHairSprite();
			break;
		case 2:
			Cursor.visible = false;
			DeactivateCrossHairSprite();
			break;
		case 3:
			Cursor.visible = false;
			ActivateCrossHairSprite();
			break;
		case 4:
			Cursor.visible = true;
			DeactivateCrossHairSprite();
			break;
		default:
			break;
		}
	}

	public void ActivateCrossHairSprite()
	{
		crossHairSprite.GetComponent<SpriteRenderer> ().enabled = true;
	}

	public void DeactivateCrossHairSprite()
	{
		crossHairSprite.GetComponent<SpriteRenderer> ().enabled = false;
	}
}
