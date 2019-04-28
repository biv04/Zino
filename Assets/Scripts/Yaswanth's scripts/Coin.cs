using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin
{
    private int coins;
    // Start is called before the first frame update
    void Start()
    {
        coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(coins < 0)
        {
            coins = 0;
        }
    }

    public int getCoins()
    {
        return this.coins;
    }

    public void setCoins(int coins)
    {
        this.coins = coins;
    }

    public void collectCoin()
    {
        this.coins++;
    }

    public void collectCoin(int coins)
    {
        this.coins += coins;
    }

    public bool buyItem(int cost)
    {
        if(coins - cost > 0)
        {
            this.coins -= cost;
            return true;
        }
        else
        {
            return false;
        }
    }


}
