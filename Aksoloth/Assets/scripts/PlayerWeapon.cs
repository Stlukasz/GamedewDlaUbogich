using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Transform weapon;
    public Transform weaponEquipped;
    public Transform weaponUnequipped;
    public bool isEquipped;


    public void Update()
    {
        if(isEquipped)
        {
            weapon.position = weaponEquipped.position;
            weapon.rotation = weaponEquipped.rotation;
        }
        else
        {
            weapon.position = weaponUnequipped.position;
            weapon.rotation = weaponUnequipped.rotation;
        }

        
        
    }
    public void Arming()
    {
        isEquipped = true;

    }


    public void UnArming()
    {
        isEquipped = false;

    }
}
