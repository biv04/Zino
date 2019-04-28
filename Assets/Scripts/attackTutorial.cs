using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class attackTutorial : MonoBehaviour
{

    public GameObject popup;
    private void Start()
    {
        popup.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            popup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            popup.SetActive(false);
        }
    }
}
