using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicUI : MonoBehaviour
{
    private bool menuUp = false;
    public GameObject basicMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuUp == false)
            {
                Debug.Log("Escape Key Pressed");

                menuUp = true;

                Time.timeScale = 0;

                basicMenu.SetActive(true);
            }
            else if (menuUp == true)
            {
                menuUp = false;

                Time.timeScale = 1;

                basicMenu.SetActive(false);
            }
        }
        
    }

    public void CloseMenu()
    {
        menuUp = false;

        Time.timeScale = 1;

        basicMenu.SetActive(false);

        Debug.Log("Close Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
