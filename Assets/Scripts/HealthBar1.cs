using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthBar1 : MonoBehaviour
{
    //private TextMeshProUGUI txtHealth;
    public Image healthBar;

    public Image hurtBar;

    private float speed = 0.3f;

    public PlayerHealth player;


    private float playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        // txtHealth = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {

        healthBar.fillAmount = Mathf.RoundToInt(player.health) / 100f;

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

}