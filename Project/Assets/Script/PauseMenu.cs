using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class PauseMenu : MonoBehaviour
{
    private GameInputs _input;
    [SerializeField] private XRDirectInteractor directInteractor;
    [SerializeField] private GameObject wristMenu;
    [SerializeField] private GameObject rayController;
  
    

   

    // Start is called before the first frame update
    void Start()
    {
        _input = new GameInputs();
        _input.Enable();
        _input.Menu.MenuPressed.performed += PausedButtonPressed;
    }


    private void PausedButtonPressed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //if (context.performed)
            DisplayWristUI();
    }

    public void DisplayWristUI()
    {
        if (wristMenu.activeInHierarchy == false)
        {
            directInteractor.SendHapticImpulse(0.4f, 0.6f);
            wristMenu.SetActive(true);
            rayController.SetActive(true);
        }
        else
        {
            wristMenu.SetActive(false);
            rayController.SetActive(false);
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
