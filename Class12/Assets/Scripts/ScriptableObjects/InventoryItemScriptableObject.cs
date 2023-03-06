using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryItem", menuName = "Scriptable Objects/New Item Scriptable Object", order = 1)]
public class InventoryItemScriptableObject : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public string itemType;
    public Sprite itemSprite;
    public int amountToPickup;
    public int maxAmount;
    public AudioClip pickupSound;
    public WeaponScriptableObject weaponToEquip;
}
