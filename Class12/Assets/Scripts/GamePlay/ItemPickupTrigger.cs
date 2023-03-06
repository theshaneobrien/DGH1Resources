using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickupTrigger : MonoBehaviour
{
    [SerializeField] private InventoryItemScriptableObject itemToPickup;

    private AudioSource thisAudioSource;
    private BoxCollider thisItemCollider;

    private void Start()
    {
        thisAudioSource = this.GetComponent<AudioSource>();
        thisItemCollider = this.GetComponent<BoxCollider>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        GameStateManager.Instance.GetPlayerInventory().AddItem(itemToPickup);
        if (itemToPickup.itemType == "ammo")
        {
            if (itemToPickup.weaponToEquip == GameStateManager.Instance.GetPlayerWeapon().GetCurrentlyEquippedWeaponSO())
            {
                GameStateManager.Instance.GetGamePlayUI().SetTotalAmmoText(
                    GameStateManager.Instance.GetPlayerInventory().GetNumberOfInventoryItems(itemToPickup.itemName) *
                    itemToPickup.amountToPickup);
            }
        }

        StartCoroutine(PlaySoundAndDie());
    }

    private IEnumerator PlaySoundAndDie()
    {
        thisItemCollider.enabled = false;
        thisAudioSource.PlayOneShot(itemToPickup.pickupSound);

        if (GetComponent<Renderer>())
        {
            Color c = GetComponent<Renderer>().material.color;
            for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
            {
                c.a = alpha;
                GetComponent<Renderer>().material.color = c;
            }
        }

        yield return new WaitForSeconds(itemToPickup.pickupSound.length);
        
        Destroy(this.GameObject());
    }
}
