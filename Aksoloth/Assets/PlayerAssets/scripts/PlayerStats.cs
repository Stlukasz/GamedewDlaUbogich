using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

     float MaxHealth;
     float CurrentHealth;
     float MaxSpirit;
     float CurrentSpirit;


    //sprites
    public Image healthbar;
    public Image spiritbar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void HpChange(int difrence)
    {
        CurrentSpirit += difrence;

        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }

        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }

        healthbar.fillAmount = CurrentHealth / MaxHealth;
    }

    public void SpiritChange(int difrence)
    {

        CurrentSpirit += difrence;

        if (CurrentSpirit < 0)
        {
            CurrentSpirit = 0;
        }

        if (CurrentSpirit > MaxSpirit)
        {
            CurrentSpirit = MaxSpirit;
        }

        spiritbar.fillAmount = CurrentSpirit / MaxSpirit;
    }
}
