using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Boo.Lang;
using System;


public class boss : EnemyController
{
  
    //private TextMeshProUGUI txtHealth;
    public Image healthBar;
    public Image hurtBar;
    private float healthBarSpeed = 0.3f;

    private Animator anim;

    public AudioClip hurt;
    public AudioClip death;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        health = 100;
        speed = 0.05f;
        hitTime = 1f;

    }

 
    void healthBarUpdate()
    {
        healthBar.fillAmount = Mathf.RoundToInt(health) / 100f;

        if (hurtBar.fillAmount >= healthBar.fillAmount)
        {
            hurtBar.fillAmount -= speed * Time.deltaTime;
        }
        else if (hurtBar.fillAmount <= healthBar.fillAmount)
        {
            hurtBar.fillAmount = healthBar.fillAmount;
        }

        //txtHealth.text = string.Format("{0} %", Mathf.RoundToInt(player.CurrentHealth));
    }



    public override void handleMovement()
    {
        base.handleMovement();
        healthBarUpdate();
    }
}