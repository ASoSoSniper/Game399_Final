using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OxygenFunctionality : MonoBehaviour
{
    private float oxygenAmount;
    private float roundedOxygenAmount;
    public float maxOxygen = 100f;
    public float oxygenTick = 0.001f;
    public Image oxygenProgressBar;
    public TMP_Text NumberDisplay;
    public GameObject LoseText;
    public GameObject Player;
    private ClimberHand[] handsList;


    // Start is called before the first frame update
    void Start()
    {
        oxygenAmount = maxOxygen;
        LoseText.SetActive(false);
        Player = GameObject.Find("Climber");
        handsList = FindObjectsOfType<ClimberHand>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOxygenBar();
    }

    void UpdateOxygenBar()
    {
        oxygenAmount -= oxygenTick * Time.timeScale;

        if(oxygenAmount > 0.0f)
        {
            oxygenProgressBar.rectTransform.localScale = new Vector3(oxygenProgressBar.rectTransform.localScale.x, oxygenAmount / maxOxygen, oxygenProgressBar.rectTransform.localScale.z);
            
            roundedOxygenAmount = Mathf.RoundToInt(oxygenAmount);
            NumberDisplay.text = roundedOxygenAmount.ToString() + " / " + maxOxygen.ToString();
        }
        else
        {
            NumberDisplay.text = "0 / " + maxOxygen.ToString();
            if (LoseText != null && Player != null)
            {
                LoseText.SetActive(true);
                Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                for (int i = 0; i < handsList.Length; i++)
                {
                    handsList[i].enabled = false;
                }
            }            
        }
    }

    public void GainOxygen(float oxygen)
    {
        oxygenAmount = Mathf.Clamp(oxygenAmount + oxygen, 0f, maxOxygen);
    }
}
