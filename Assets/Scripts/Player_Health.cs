using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{


    // InstaDeath objects should be tagged "Death" and set as a trigger
    // Enemies (and other 1-damage obstacles) should be tagged "Enemy" and should NOT be set as a trigger

    public GameObject respawn;

    [Tooltip("The amount of hits a player can take before respawning.")]
    public int MaxHealth = 5;
    public int health;

    public AudioClip Chirp;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;



    // Use this for initialization
    void Start()
    {
        health = MaxHealth;
    
        respawn = GameObject.FindGameObjectWithTag("Respawn");


    }

    private void Update()
    {
        if (gameObject.transform.position.y < 0)
        { Respawn();}

        if (health > MaxHealth)
        { health = MaxHealth; }

        for(int i=0; i < hearts.Length; i++)
        {
            if (i < health)
            { hearts[i].sprite = fullHeart;}

            else
            { hearts[i].sprite = emptyHeart; }

            if (i < MaxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = true;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
       
        if (collision.CompareTag("Finish"))
        {
            Time.timeScale = 0;
        }

     

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            TakeDamage();
        }
        if (collision.collider.CompareTag("Hazard"))
        {
            TakeDamage();
        }
        if (collision.collider.CompareTag("Heal"))
        {
            Debug.Log("Position b");
            heal();
        }
    }

    private void TakeDamage()
    {
        health = health - 1;
        if (health <= 0)
        {
            Respawn();
        }
    }


    private void heal()
    {
        health = health + 1;
    }

    public void Respawn()
    {
        health = MaxHealth;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.transform.position = respawn.transform.position;
      
    }


  
}
