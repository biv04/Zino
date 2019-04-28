using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeWeapon(Weapon w)
    {
        w.UpgradeWeapon();
    }

    
    //TODO: We shouldn't use this as the coins storage. We will get the player object and use them.
    
//    public void BuyWeapon(AbstractItem a)
//    {
//        if (coin > a.GetCost())
//        {
//            coin -= a.GetCost();
//            a.SetPurchased(true);
//        }
//    }
}

