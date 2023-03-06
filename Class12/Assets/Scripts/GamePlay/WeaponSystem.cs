using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

// Here is the commented code: https://gist.github.com/theshaneobrien/a193dea6285200846c4b6f04d685367c
public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private GameObject reticleImage;
    [SerializeField] private AudioSource gunSoundSource;
    [SerializeField] private WeaponScriptableObject currentEquippedWeaponSO;
    [SerializeField] private List<WeaponScriptableObject> availableWeaponSOs;

    private GamePlayUI gamePlayUI;

    private int currentLoadedAmmo = 0;
    private int currentSelectedWeaponIndex = 0;
    private int lastSelectedWeaponIndex = 0;
    
    private float timeSpentReloading = 0;
    private float timeBetweenShots = 0;

    private bool isReloading = false;
    private bool isFiring = true;
    
    //This is our input variables
    private PlayerControls playerInput;

    private void Awake()
    {
       playerInput = new PlayerControls();
    }

    private void OnEnable()
    {
        playerInput.Enable();
        //We put our Performed / Cancelled

        playerInput.PlayerWeapons.FireWeapon.started += OnFireWeaponStarted;
        playerInput.PlayerWeapons.FireWeapon.canceled += OnFireWeaponCanceled;

        playerInput.PlayerWeapons.Reload.started += OnReloadStarted;

        playerInput.PlayerWeapons.ChangeWeaponNext.started += OnChangeWeaponNext;
        playerInput.PlayerWeapons.ChangeWeaponPrevious.started += OnChangeWeaponPrevious;
    }

    private void OnDisable()
    {
        playerInput.Disable();

        playerInput.PlayerWeapons.FireWeapon.started -= OnFireWeaponStarted;
        playerInput.PlayerWeapons.FireWeapon.canceled -= OnFireWeaponCanceled;
        
        playerInput.PlayerWeapons.Reload.started -= OnReloadStarted;

        playerInput.PlayerWeapons.ChangeWeaponNext.started -= OnChangeWeaponNext;
        playerInput.PlayerWeapons.ChangeWeaponPrevious.started -= OnChangeWeaponPrevious;
    }

    private void OnChangeWeaponNext(InputAction.CallbackContext value)
    {

        currentSelectedWeaponIndex += 1;
        if (currentSelectedWeaponIndex >= 3)
        {
            currentSelectedWeaponIndex = 0;
        }
        ChangeWeapon(currentSelectedWeaponIndex);
    }

    private void OnChangeWeaponPrevious(InputAction.CallbackContext value)
    {
        currentSelectedWeaponIndex -= 1;
        if (currentSelectedWeaponIndex <= -1)
        {
            currentSelectedWeaponIndex = 2;
        }
        ChangeWeapon(currentSelectedWeaponIndex);
    }

    private void OnFireWeaponStarted(InputAction.CallbackContext value)
    {
        if (currentEquippedWeaponSO.fireType == "auto")
        {
            isFiring = true;
        }
        else
        {
            
            CheckFireDelay();  
        }
    }

    private void OnFireWeaponCanceled(InputAction.CallbackContext value)
    {
        isFiring = false;
    }

    private void OnReloadStarted(InputAction.CallbackContext value)
    {
        Reload();
    }

    private void Start()
    {
        gamePlayUI = GameStateManager.Instance.GetGamePlayUI();
        
        gamePlayUI.SetWeaponNameText(currentEquippedWeaponSO.weaponName);
        gamePlayUI.SetWeaponUIImage(currentEquippedWeaponSO.weaponIcon);
        
        
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

            if (isFiring)
            {
                CheckFireDelay();    
            }
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
        if (currentEquippedWeaponSO.fireType == "semi")
        {
            if (Input.GetButtonDown("Fire1"))
            {
            }
        }

        if (currentEquippedWeaponSO.fireType == "auto")
        {
            if (Input.GetButton("Fire1"))
            {
                CheckFireDelay();
            }
        }

        if (Input.GetButtonDown("Reload"))
        {
        }

        if (Input.GetButtonDown("Weapon1"))
        {
            ChangeWeapon(0);
        }

        if (Input.GetButtonDown("Weapon2"))
        {
            ChangeWeapon(1);
        }

        if (Input.GetButtonDown("Weapon3"))
        {
            ChangeWeapon(2);
        }

        if (Input.GetButtonDown("HotswapWeapon"))
        {
            HotswapWeapons();
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
            gunSoundSource.PlayOneShot(currentEquippedWeaponSO.triggerSound);
        }
    }

    private void FireGun()
    {
        gunSoundSource.PlayOneShot(currentEquippedWeaponSO.weaponFireSounds[Random.Range(0, currentEquippedWeaponSO.weaponFireSounds.Length - 1)]);
        
        RaycastHit hitObject;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hitObject, 100))
        {
            if (hitObject.collider != null)
            {
                if (hitObject.collider.tag == "Enemy")
                {
                    //Cause the enemy to take damage
                    hitObject.collider.GetComponent<EnemyHealth>().TakeDamage(currentEquippedWeaponSO.weaponBaseDamage);
                }
            }
        }
        
        // This will subtract current ammo by 1
        currentLoadedAmmo--;
        SetGunUIElements();
        
        // Now increase our delay between firing
        timeBetweenShots = currentEquippedWeaponSO.fireDelay;
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
        gunSoundSource.PlayOneShot(currentEquippedWeaponSO.reloadSound);
        isReloading = true;
    }

    private void DoReload()
    {
        if (isReloading == true)
        {
            timeSpentReloading += Time.deltaTime;
            if (GameStateManager.Instance.GetPlayerInventory().CheckItemExists(currentEquippedWeaponSO.ammoInventoryItem) >= 0)
            {
                if (timeSpentReloading > currentEquippedWeaponSO.reloadTime)
                {
                    currentLoadedAmmo = currentEquippedWeaponSO.maxAmmoCount;
                    GameStateManager.Instance.GetPlayerInventory().RemoveItem(currentEquippedWeaponSO.ammoInventoryItem);
                    SetGunUIElements();
                    timeSpentReloading = 0;
                    isReloading = false;
                }
            }
        }
    }

    private void SetGunUIElements()
    {
        gamePlayUI.SetCurrentAmmoAmount(currentLoadedAmmo, currentEquippedWeaponSO.maxAmmoCount);
        gamePlayUI.SetTotalAmmoText(GameStateManager.Instance.GetPlayerInventory().GetNumberOfInventoryItems(currentEquippedWeaponSO.ammoInventoryItem.itemName) * currentEquippedWeaponSO.ammoInventoryItem.amountToPickup);
        
    }

    public WeaponScriptableObject GetCurrentlyEquippedWeaponSO()
    {
        return currentEquippedWeaponSO;
    }

    public void SetCurrentlyEquippedWeaponSO(WeaponScriptableObject weaponToEquip)
    {
        currentEquippedWeaponSO = weaponToEquip;
        currentLoadedAmmo = weaponToEquip.maxAmmoCount;
        SetGunUIElements();
        gamePlayUI.SetWeaponNameText(weaponToEquip.weaponName);
        gamePlayUI.SetWeaponUIImage(weaponToEquip.weaponIcon);
        
        
    }

    public void CheckWhetherToAddWeaponOrAmmo(WeaponScriptableObject weaponToAdd)
    {
        if (availableWeaponSOs.IndexOf(weaponToAdd) == -1)
        {
            //Add this weapon to available weapons
            AddToAvailableWeapons(weaponToAdd);
            GameStateManager.Instance.GetPlayerInventory().AddItem(weaponToAdd.ammoInventoryItem);
            SetCurrentlyEquippedWeaponSO(weaponToAdd);
        }
        else
        {
            //TODO: Grant player the Max Ammo Amount to their current ammo;
            GameStateManager.Instance.GetPlayerInventory().AddItem(weaponToAdd.ammoInventoryItem);
        }
        
    }

    public void AddToAvailableWeapons(WeaponScriptableObject weaponToAdd)
    {
        availableWeaponSOs[weaponToAdd.weaponArraySlot] = weaponToAdd;
    }

    private void ChangeWeapon(int weaponToChangeTo)
    {
        if (availableWeaponSOs[weaponToChangeTo] != null)
        {
            SetCurrentlyEquippedWeaponSO(availableWeaponSOs[weaponToChangeTo]);
            
            lastSelectedWeaponIndex = currentSelectedWeaponIndex;
            currentSelectedWeaponIndex = availableWeaponSOs[weaponToChangeTo].weaponArraySlot;
        }
    }

    private void HotswapWeapons()
    {
        // This is a very fancy way of swapping the two numbers
        (currentSelectedWeaponIndex, lastSelectedWeaponIndex) = (lastSelectedWeaponIndex, currentSelectedWeaponIndex);

        SetCurrentlyEquippedWeaponSO(availableWeaponSOs[currentSelectedWeaponIndex]);
    }
}
