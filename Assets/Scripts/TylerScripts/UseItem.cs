using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UseItem : MonoBehaviour {


    public int cost;
    public int length;
    public bool appliedOverTime;
    
    
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void activate();

}


public class HealthPotion : UseItem {

    void Start() {

        cost = 10;
        appliedOverTime = false;

    }

    public override void activate() {

        GetComponent<PlayerHealth>().health += 50;
        if (GetComponent<PlayerHealth>().health > 100) {
            GetComponent<PlayerHealth>().health = 100;
        }
    }
    
    
    
    
}


public class SpeedPotion : UseItem {

    //TODO: HANDLE APPLIED OVER TIME
    
    void Start() {

        cost = 15;
        appliedOverTime = true;
        length = 10;
    }
    public override void activate() {
        GetComponent<PlayerMovement>().speed += 0.02f;
        var coroutine = finishPotion(Time.time);
        StartCoroutine(coroutine);

    }

    private IEnumerator finishPotion(float time) {

        while (Time.time < time + length) {
            yield return 0;
        }
        
        
        GetComponent<PlayerMovement>().speed -= 0.02f;

    }
    
    
}