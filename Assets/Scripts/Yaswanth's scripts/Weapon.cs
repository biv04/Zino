using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    private int cost;
    private string nameWeapon;
    private bool purchased;
    private int damage;
    private int level;
    private bool range;
    // Start is called before the first frame update
    void Start()
    {
        cost = 0;
        nameWeapon = "";
        purchased = false;
        damage = 0;
        level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetCost()
    {
        return cost;
    }

    public void SetCost(int cost)
    {
        this.cost = cost;
    }

    public string GetNameWeapon()
    {
        return nameWeapon;
    }

    public void SetNameWeapon(string name)
    {
        this.nameWeapon = name;
    }

    public bool GetPurchased()
    {
        return purchased;
    }

    public void SetPurchased(bool purchased)
    {
        this.purchased = purchased;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }
    public bool GetRange()
    {
        return range;
    }

    public void SetRange(bool range)
    {
        this.range = range;
    }


    public void UpgradeWeapon()
    {
        level++;
        damage += 10;
    }

    public void CalcCost()
    {
        if (range)
        {
            this.SetCost((8 * this.GetLevel() + 2) * 5 / 4);
        }
        else
        {
            this.SetCost((8 * this.GetLevel() + 2));
        }

    }
}
