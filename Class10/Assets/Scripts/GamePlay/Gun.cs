using System;
using UnityEngine;
using Random = UnityEngine.Random;

// Here is the commented code: https://gist.github.com/theshaneobrien/a193dea6285200846c4b6f04d685367c
public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject reticleImage;
    [SerializeField] private AudioSource gunSoundSource;
    [SerializeField] private GunScriptableObject gunSO;

    private GamePlayUI gamePlayUI;

    private int currentLoadedAmmo = 0;
    
    private float timeSpentReloading = 0;
    private float timeBetweenShots = 0;

    private bool isReloading = false;

    private void Start()
    {
        gamePlayUI = GameStateManager.Instance.GetGamePlayUI();
        
        gamePlayUI.SetGunNameText(gunSO.gunName);
        SetGunUIElements();
    }

    private void Update()
    {
        if (GameStateManager.Instance.GetPlayerIsReady() == true && GameStateManager.Instance.GetPlayerWon() == false)
        {
            EnableGunReticle();
            DetectInput();
            
            DoReload();
            FireDelayCountdown();
        }
    }

    private void EnableGunReticle()
    {
        if (reticleImage.activeSelf == false)
        {
            reticleImage.SetActive(true);
        }
    }

    //This is happening every single frame
    private void DetectInput()
    {
        if (gunSO.fireType == "semi")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                CheckFireDelay();
            }
        }

        if (gunSO.fireType == "auto")
        {
            if (Input.GetButton("Fire1"))
            {
                CheckFireDelay();
            }
        }

        if (Input.GetButtonDown("Reload"))
        {
            Reload();
        }
    }
    
    private void CheckFireDelay()
    {
        if (timeBetweenShots == 0)
        {
            CheckAmmo();
        }
    }
    
    private void CheckAmmo()
    {
        if (currentLoadedAmmo > 0)
        {
            FireGun();
        }
        else
        {
            gunSoundSource.PlayOneShot(gunSO.triggerSound);
        }
    }

    private void FireGun()
    {
        gunSoundSource.PlayOneShot(gunSO.gunFireSounds[Random.Range(0, gunSO.gunFireSounds.Length - 1)]);
        
        RaycastHit hitObject;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hitObject, 100))
        {
            if (hitObject.collider != null)
            {
                if (hitObject.collider.tag == "Enemy")
                {
                    //Cause the enemy to take damage
                    hitObject.collider.GetComponent<EnemyHealth>().TakeDamage(gunSO.gunBaseDamage);
                }
            }
        }
        
        // This will subtract current ammo by 1
        currentLoadedAmmo--;
        SetGunUIElements();
        
        // Now increase our delay between firing
        timeBetweenShots = gunSO.fireDelay;
    }
    
    private void FireDelayCountdown()
    {
        if (timeBetweenShots <= 0)
        {
            timeBetweenShots = 0;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }
    
    private void Reload()
    {
        gunSoundSource.PlayOneShot(gunSO.reloadSound);
        isReloading = true;
    }

    private void DoReload()
    {
        if (isReloading == true)
        {
            timeSpentReloading += Time.deltaTime;
            if (GameStateManager.Instance.GetPlayerInventory().CheckItemExists(gunSO.ammoInventoryItem) >= 0)
            {
                if (timeSpentReloading > gunSO.reloadTime)
                {
                    currentLoadedAmmo = gunSO.maxAmmoCount;
                    GameStateManager.Instance.GetPlayerInventory().RemoveItem(gunSO.ammoInventoryItem);
                    SetGunUIElements();
                    timeSpentReloading = 0;
                    isReloading = false;
                }
            }
        }
    }

    private void SetGunUIElements()
    {
        gamePlayUI.SetCurrentAmmoAmount(currentLoadedAmmo, gunSO.maxAmmoCount);
        gamePlayUI.SetTotalAmmoText(GameStateManager.Instance.GetPlayerInventory().GetNumberOfInventoryItems(gunSO.ammoInventoryItem.itemName) * gunSO.ammoInventoryItem.amountToPickup);
    }
}
