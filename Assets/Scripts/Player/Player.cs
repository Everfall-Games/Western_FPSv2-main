using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IDamageable
{
    //TO-DO 
    //Find a way to retrieve the currentWeaponType
    private WeaponsManager.WeaponType currentWeapon;
    private int health=100;
   
    void Start()
    {
        //Initializing the weapon to a rifle, for now...
        currentWeapon = WeaponsManager.WeaponType.RIFLE;
    }

    
    void Update()
    {
        //If press shoot button
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shooting");
            Shoot();
        }

        if(health<=0)
        {
            Die();
        }
    }

    //Interface implementation on the player
    public void TakeDamage(int weaponDamage, GameObject whoInflictedDamage)
    {
        Debug.Log("Taking damage, health is " + health);
        health -= weaponDamage;
    }

    private void Die()
    {
        //To-Do
        Debug.Log("Player is dead...");
    }
    private void Shoot()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        //Cast a ray to the middle of the screen
        if(Physics.Raycast(ray,out hit))
        {
            //If the ray hits an Animal
            if(hit.transform.CompareTag("Animal"))
            {
                //Getting damageable interface from animal
                var damageable = hit.transform.gameObject.GetComponent<IDamageable>();
                if (damageable == null) return;

                //Takes damage
                damageable.TakeDamage(WeaponsManager.GetWeaponDamage(currentWeapon),this.gameObject);
                Debug.Log("We are hitting a animal");
            }
            //If teh ray hits a Player
            else if (hit.transform.CompareTag("Player"))
            {
                var damageable = hit.transform.gameObject.GetComponent<IDamageable>();
                if (damageable == null) return;
                //Takes damage
                damageable.TakeDamage(WeaponsManager.GetWeaponDamage(currentWeapon), this.gameObject);
            }
        }
    }
}
