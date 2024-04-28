using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PauseMenu : MonoBehaviour
{
   // private GameInputs _input;
   // [SerializeField] private XRDirectInteractor directInteractor;
   // [SerializeField] private GameObject wristMenu;
   //[SerializeField] private GameObject rayController;

    public GameObject wristUI;


    public bool activeWristUI = true;
    // Start is called before the first frame update
    void Start()
    {
        /*_input = new GameInputs();
        _input.Enable();
        _input.Menu.MenuPressed.performed += PausedButtonPressed;*/

        DisplayWristUI();
    }


    public void PausedButtonPressed(InputAction.CallbackContext obj)
    {
       if(obj.performed)
        DisplayWristUI();
    }

    public void DisplayWristUI()
    {
        /*if (wristMenu.activeInHierarchy == false)
        {
            directInteractor.SendHapticImpulse(0.4f, 0.6f);
            wristMenu.SetActive(true);
            rayController.SetActive(true);
        }
        else
        {
            wristMenu.SetActive(false);
            rayController.SetActive(false);
        }*/

        if(activeWristUI)
        {
            wristUI.SetActive(false);
            activeWristUI = false;
        }
        else if (!activeWristUI)
        {
            wristUI.SetActive(true);
            activeWristUI = true;
        }
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restart Level");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
