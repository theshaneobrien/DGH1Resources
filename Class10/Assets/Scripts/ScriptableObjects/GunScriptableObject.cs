using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGunSO", menuName = "Scriptable Objects/New Gun Scriptable Object", order = 2)]
public class GunScriptableObject : ScriptableObject
{
    public string gunName;
    public int maxAmmoCount;
    public float reloadTime;
    public float fireDelay;
    public float gunBaseDamage;
    public InventoryItemScriptableObject ammoInventoryItem;
    
    public AudioClip[] gunFireSounds;
    public AudioClip reloadSound;
    public AudioClip triggerSound;
}
