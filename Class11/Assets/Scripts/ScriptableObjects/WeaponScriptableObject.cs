using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewWeaponSO", menuName = "Scriptable Objects/New Weapon Scriptable Object", order = 2)]
public class WeaponScriptableObject : ScriptableObject
{
    [FormerlySerializedAs("gunName")] public string weaponName;
    public int weaponArraySlot;
    public int maxAmmoCount;
    public float reloadTime;
    public float fireDelay;
    [FormerlySerializedAs("gunBaseDamage")] public float weaponBaseDamage;
    public InventoryItemScriptableObject ammoInventoryItem;
    public string fireType;
    public Sprite weaponIcon;
    [FormerlySerializedAs("gunFireSounds")] public AudioClip[] weaponFireSounds;
    public AudioClip reloadSound;
    public AudioClip triggerSound;
}
