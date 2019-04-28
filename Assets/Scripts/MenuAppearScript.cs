using UnityEngine;
using System.Collections;

public class MenuAppearScript : MonoBehaviour
{

    public GameObject menu; // Assign in inspector
    private bool isShowing;
    private bool resume;
    private void Start()
    {
        resume = false;
        isShowing = false;
        menu.SetActive(isShowing);

    }
    void Update()
    {
        if (Input.GetKeyDown("escape") || resume == true)
        {
            resume = false;
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }
        if (isShowing) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void res()
    {
        resume = true;
    }

    public void exit()
    {
        Application.Quit();
    }

}