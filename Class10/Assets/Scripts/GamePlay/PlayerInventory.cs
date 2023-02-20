using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private PlayerInventoryScriptableObject playerInvSO;

    public void AddItem(InventoryItemScriptableObject itemToAdd)
    {
        playerInvSO.GetFullInventory().Add(itemToAdd);
    }

    public void RemoveItem(InventoryItemScriptableObject itemToRemove)
    {
        playerInvSO.GetFullInventory().Remove(itemToRemove);
    }

    public int CheckItemExists(InventoryItemScriptableObject itemToCheck)
    {
        return playerInvSO.GetFullInventory().IndexOf(itemToCheck);
    }

    public InventoryItemScriptableObject GetItemFromIndex(int indexOfItem)
    {
        return playerInvSO.GetFullInventory()[indexOfItem];
    }

    public int GetNumberOfInventoryItems(string itemToCount)
    {
        int itemCount = 0;
        foreach (InventoryItemScriptableObject item in playerInvSO.GetFullInventory())
        {
            if (item.itemName == itemToCount)
            {
                itemCount++;
            }
        }

        return itemCount;
    }

    public void ClearInventory()
    {
        playerInvSO.GetFullInventory().Clear();
    }
}
