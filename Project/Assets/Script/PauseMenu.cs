using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PauseMenu : MonoBehaviour
{
    private GameInputs _input;
    [SerializeField] private XRDirectInteractor directInteractor;
    [SerializeField] private GameObject wristMenu;
    [SerializeField] private GameObject rayController;
    public GameObject wristUI;
    

    public bool activeWristUI = true;

    // Start is called before the first frame update
    void Start()
    {
        _input = new GameInputs();
        _input.Enable();
        _input.Menu.MenuPressed.performed += PausedButtonPressed;
    }


    public void PausedButtonPressed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //if (context.performed)
            DisplayWristUI();
    }

    public void DisplayWristUI()
    {
        if (activeWristUI)
        {
            wristUI.SetActive(false);
            activeWristUI = false;
            Time.timeScale = 1;
        }
        else if (!activeWristUI)
        {
            wristUI.SetActive(true);
            activeWristUI = true;
            Time.timeScale = 0;
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
