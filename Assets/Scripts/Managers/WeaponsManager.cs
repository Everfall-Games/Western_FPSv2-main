using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponsManager : MonoBehaviour
{
    //Get weapon damage
    public static int GetWeaponDamage(WeaponType currentWeapon)
    {
        return weapons[currentWeapon];
    }
    public enum WeaponType
    {
        RIFLE,
        REVOLVER,
        NUM_WEAPONS
    }

    //Dictionary to hold our weapons and our respective damage values
    private static Dictionary<WeaponType, int> weapons = new Dictionary<WeaponType, int>();
    private void Awake()
    {
        //Initializing weapon damage values
        weapons.Add(WeaponType.RIFLE, 50);
        weapons.Add(WeaponType.REVOLVER, 25);
    }
}
