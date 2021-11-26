using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    AnimationControl animationControl;

    public Transform weapon;
    public Transform weaponEquipped;
    public Transform weaponUnequipped;
    public bool isEquipped;

    public GameObject slashTour;



    private void Awake()
    {       
        animationControl = GetComponent<AnimationControl>();
    }

    public void Update()
    {

        if(animationControl.animator.GetBool("isAtacking") || animationControl.animator.GetBool("atack2") || animationControl.animator.GetBool("atack3"))
        {
            slashTour.SetActive(true);      
        }
        else
        {
            slashTour.SetActive(false);
        }


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
