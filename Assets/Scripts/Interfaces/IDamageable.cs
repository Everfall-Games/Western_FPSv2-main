using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Good to use interface in case we ever wanna apply damage in a different way depending on the object taking the damage
public interface IDamageable 
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="weaponDamage">The weapon damage so we know the amount of health to decrease/param>
    /// <param name="whoInflictedDamage">Which game object inflicted the damage</param>
    void TakeDamage(int weaponDamage, GameObject whoInflictedDamage);

}
