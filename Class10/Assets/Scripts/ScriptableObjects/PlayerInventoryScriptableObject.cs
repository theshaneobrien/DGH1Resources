using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerInventory", menuName = "Scriptable Objects/New PlayerInventory Scriptable Object", order = 1)]
public class PlayerInventoryScriptableObject : ScriptableObject
{
    [SerializeField]
    private List<InventoryItemScriptableObject> playerInventoryItems = new List<InventoryItemScriptableObject>();

    public List<InventoryItemScriptableObject> GetFullInventory()
    {
        return playerInventoryItems;
    }
}
