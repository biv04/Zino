using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Coin balance = new Coin();
    public List<Weapon> backpack = new List<Weapon>();
    public List<UseItem> items = new List<UseItem>();
    Weapon originalSword = new Weapon();
    Weapon originalBow = new Weapon();
    private int currWeapon = 0;

    // Start is called before the first frame update
    void Awake()
    {
        originalSword.SetDamage(10);
        originalBow.SetDamage(10);
        balance.setCoins(0);
        backpack.Add(originalSword);
        backpack.Add(originalBow);
    }

    
    public void upgradeItem(Weapon weapon)
    {
        weapon.UpgradeWeapon();


    }

    public Weapon SwitchActiveWeapon()
    {
        if (currWeapon == 0) {
            currWeapon = 1; 
            Cursor.visible = true;
        }
        else {
            currWeapon = 0;
            Cursor.visible = false;
        }

        return backpack[currWeapon];

    }


    public Weapon getCurrWeapon() {
        return backpack[currWeapon];
    }
    
    
}
