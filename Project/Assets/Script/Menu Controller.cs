using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OptionButton()
    {

    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Application quit");
    }
}
